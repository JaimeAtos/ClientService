using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Consumers.CatalogLocationConsumers;
using Application.Consumers.CatalogReasonsConsumers;
using Application.Consumers.CatalogStateConsumers;
using Application.Consumers.PositionConsumers;
using Application.Consumers.ResourceConsumers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.Commons.Publishers;
using Atos.Core.EventsDTO;
using MassTransit;

namespace Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IPublisherCommands<ClientUpdated>, PublisherCommands<ClientUpdated>>();
            services.AddScoped<IPublisherCommands<ClientDeleted>, PublisherCommands<ClientDeleted>>();
            services.AddScoped<IPublisherCommands<ClientPositionUpdated>, PublisherCommands<ClientPositionUpdated>>();
            services.AddScoped<IPublisherCommands<ClientPositionDeleted>, PublisherCommands<ClientPositionDeleted>>();
            return services;
        }

        public static IServiceCollection AddMassTransitConfig(this IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<PositionUpdatedConsumer>();
                cfg.AddConsumer<PositionDeletedConsumer>();
                cfg.AddConsumer<CatalogLocationUpdatedConsumer>();
                cfg.AddConsumer<CatalogLocationDeletedConsumer>();
                cfg.AddConsumer<CatalogStateUpdatedConsumer>();
                cfg.AddConsumer<CatalogStateDeletedConsumer>();
                cfg.AddConsumer<CatalogReasonsUpdatedConsumer>();
                cfg.AddConsumer<CatalogReasonsDeletedConsumer>();
                cfg.AddConsumer<ResourceUpdatedConsumer>();
                cfg.AddConsumer<ResourceDeletedConsumer>();

                cfg.UsingRabbitMq((ctx, cfgrmq) =>
                {
                    cfgrmq.Host(GetMessageBrokerUrl());
                    cfgrmq.ReceiveEndpoint("ClientServiceQueue", econfigureEndpoint =>
                    {
                        econfigureEndpoint.ConfigureConsumeTopology = false;
                        econfigureEndpoint.Durable = true;
                        econfigureEndpoint.ConfigureConsumer<PositionUpdatedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<PositionDeletedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogLocationUpdatedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogLocationDeletedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogStateUpdatedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogStateDeletedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogReasonsUpdatedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<CatalogReasonsDeletedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<ResourceUpdatedConsumer>(ctx);
                        econfigureEndpoint.ConfigureConsumer<ResourceDeletedConsumer>(ctx);

                        econfigureEndpoint.UseMessageRetry(retryConfigure =>
                        {
                            retryConfigure.Interval(5, TimeSpan.FromMilliseconds(1000));
                        });

                        econfigureEndpoint.Bind("Atos.Core.EventsDTO:PositionUpdated", d =>
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
                        econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogReasonsUpdated", d =>
                        {
                            d.ExchangeType = "topic";
                            d.RoutingKey = "catalog.reasons.updated";
                        });
                        econfigureEndpoint.Bind("Atos.Core.EventsDTO:CatalogReasonsDeleted", d =>
                        {
                            d.ExchangeType = "topic";
                            d.RoutingKey = "catalog.reasons.deleted";
                        });
                        econfigureEndpoint.Bind("Atos.Core.EventsDTO:ResourceUpdated", d =>
                        {
                            d.ExchangeType = "topic";
                            d.RoutingKey = "resource.updated";
                        });
                        econfigureEndpoint.Bind("Atos.Core.EventsDTO:ResourceDeleted", d =>
                        {
                            d.ExchangeType = "topic";
                            d.RoutingKey = "resource.deleted";
                        });
                    });

                    cfgrmq.Publish<ClientUpdated>(x => { x.ExchangeType = "topic"; });
                    cfgrmq.Publish<ClientDeleted>(x => { x.ExchangeType = "topic"; });
                    cfgrmq.Publish<ClientPositionUpdated>(x => { x.ExchangeType = "topic"; });
                    cfgrmq.Publish<ClientPositionDeleted>(x => { x.ExchangeType = "topic"; });
                });
            });

            return services;
        }
        
        private static string GetMessageBrokerUrl()
        {
            var messageBrokerHost = Environment.GetEnvironmentVariable("MQHOST");
            var messageBrokerPort = Environment.GetEnvironmentVariable("MQPORT");
            var user = Environment.GetEnvironmentVariable("MQUSER");
            var password = Environment.GetEnvironmentVariable("MQPASSWORD");
            var url = $"amqp://{user}:{password}@{messageBrokerHost}:{messageBrokerPort}";
            return url;
        }
    }
}