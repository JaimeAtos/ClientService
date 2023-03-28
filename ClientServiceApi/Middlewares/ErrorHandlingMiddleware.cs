using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace ClientServiceApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error.Message };

                switch (error)
                {
                    case Application.Exceptions.ApiExceptions e:
                        //Custom Aplication Error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
          
                        break;

                    case Application.Exceptions.ValidationException e:
                        // Custom Application
                        response.StatusCode = (int)HttpStatusCode.BadRequest;

                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        //Not Found Error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                     
                        break;
                    default:
                        //unhandle Error 
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);

            }
        }
    }
}
