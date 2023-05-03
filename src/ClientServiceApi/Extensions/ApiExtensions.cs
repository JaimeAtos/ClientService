using ClientServiceApi.Middlewares;

namespace ClientServiceApi.Extensions
{
    public static class ApiExtensions
    {
        public static void useErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
