using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence
{
    public static class DependencyContainer 
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString(GetDefaultConnectionString());
            services.AddDbContext<ClientContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            return services;
        }

        private static string GetDefaultConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var user = Environment.GetEnvironmentVariable("DBUSER");
            var pass = Environment.GetEnvironmentVariable("DBPASSWORD");
            var db = Environment.GetEnvironmentVariable("DBNAME");
            var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass};";
            return connectionString;
        }
    }
}
