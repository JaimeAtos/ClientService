using APIConfigs;
using APIConfigs.Policies;
using Application;
using Controllers;
using Controllers.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddSolutionDependencies(this IServiceCollection services)
    {
        return services
            .AddPersistenceLayer()
            .AddApplicationLayer()
            .AddMassTransitConfig()
			.AddMicroservicesCors()
            .AddControllersAndApiVersioning();
    }
    
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
		app.UseCors(ConsumePolicy.FrontPolicy.ToString());
    }
}

