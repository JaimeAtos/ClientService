using Application.Behaviours;
using Application.Features.Client.Commands.CreateClientCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;

namespace Application
{
	public static class DependencyContainer
	{
		public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(cfg =>
				cfg.RegisterServicesFromAssembly(typeof(CreateClientCommand).Assembly));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			services.AddMassTransit(cfg =>
			{
				cfg.AddConsumer<PositionCreatedConsumer>();
				cfg.UsingRabbitMq((ctx, cfgrmq) =>
				{
					cfgrmq.Host("amqp://guest:guest@localhost:5672");
					cfgrmq.ReceiveEndpoint("ClientServiceQueue", econfigureEndpoint =>
					{
						econfigureEndpoint.ConfigureConsumeTopology = false;
						econfigureEndpoint.Durable = true;
						econfigureEndpoint.UseMessageRetry(retryConfigure =>
						{
							retryConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
						});
						econfigureEndpoint.ConfigureConsumer<PositionCreatedConsumer>(ctx);

						econfigureEndpoint.Bind("Atos.Core.EventsDTO:PositionCreated", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "position.created";
						});
					});
				});
			});
		}
	}
}