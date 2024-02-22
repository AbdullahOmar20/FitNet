

using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
        IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync (HttpContext context){
            try{
                //no exception => request will continue to next stage middleware
                await _next(context);
            }
            catch(Exception ex){
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType= "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment()
                ? new APIException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                : new APIResponse((int)HttpStatusCode.InternalServerError);
                
                var option = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,option);
                await context.Response.WriteAsync(json);
            }
        }
    }
}