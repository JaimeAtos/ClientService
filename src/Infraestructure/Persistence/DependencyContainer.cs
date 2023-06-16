using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyContainer 
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
        {
            return services
                .AddDbContext<ClientContext>(options => options.UseNpgsql(GetConnectionString()))
                .AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IClientPositionRepository, ClientPositionRepository>()
                .AddScoped<IClientPositionManagerRepository, ClientPositionManagerRepository>()
                .AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        }
        
        private static string GetConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var user = Environment.GetEnvironmentVariable("DBUSER");
            var password = Environment.GetEnvironmentVariable("DBPASSWORD");
            var dbname = Environment.GetEnvironmentVariable("DBNAME");
            var connectionString = $"Username={user};Password={password};Host={host};Port={port};Database={dbname};";
            return connectionString;
        }

    }
}
