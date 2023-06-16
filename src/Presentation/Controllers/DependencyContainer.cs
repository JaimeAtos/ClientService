using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Controllers;

public static class DependencyContainer
{
    public static IServiceCollection AddControllersAndApiVersioning(this IServiceCollection services)
    {
        return services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
            })
            .AddControllers().Services;
    }
}