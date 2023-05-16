using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.Commons.Publishers;
using Atos.Core.EventsDTO;
using MassTransit;

namespace Application
{
	public static class DependencyContainer
	{
		public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			
			services.AddScoped<IPublisherCommands<ClientUpdated>, PublisherCommands<ClientUpdated>>();
			services.AddScoped<IPublisherCommands<ClientDeleted>, PublisherCommands<ClientDeleted>>();
			services.AddScoped<IPublisherCommands<ClientPositionUpdated>, PublisherCommands<ClientPositionUpdated>>();
			services.AddScoped<IPublisherCommands<ClientPositionDeleted>, PublisherCommands<ClientPositionDeleted>>();

			services.AddMassTransit(cfg =>
			{

				
				
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

						econfigureEndpoint.Bind("Atos.Core.EventsDTO:ClientUpdated", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "position.updated";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:PositionDeleted", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "position.deleted";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogLocationUpdated", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.location.updated";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogLocationDeleted", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.location.deleted";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogStateUpdated", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.state.updated";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogStateDeleted", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.state.deleted";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogStateUpdated", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.reasons.updated";
						});
						econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogStateDeleted", d =>
						{
							d.ExchangeType = "topic";
							d.RoutingKey = "catalog.reasons.deleted";
						});
					});
					
					cfgrmq.Publish<ClientUpdated>(x =>
					{
						x.ExchangeType = "topic";
					});
					cfgrmq.Publish<ClientDeleted>(x =>
					{
						x.ExchangeType = "topic";
					});
					cfgrmq.Publish<ClientPositionUpdated>(x =>
					{
						x.ExchangeType = "topic";
					});
					cfgrmq.Publish<ClientPositionDeleted>(x =>
					{	
						x.ExchangeType = "topic";
					});
					
				});
			});
		}
	}
}