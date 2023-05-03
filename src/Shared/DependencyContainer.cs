using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
    public static class DependencyContainer
    {
        public static void AddSharedLayer(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
