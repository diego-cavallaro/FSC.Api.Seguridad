using FSC.Api.Mantenimiento.Errors;
using FSC.Api.Mantenimiento.Validation;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace FSC.Api.Mantenimiento.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un errror inerperado. {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;

            String message = exception switch
            {
                CustomException => "Error de Validación",
                InvalidOperationException => "Error de Operación",
                _ => "Error Interno No Esperado"
            };
            Int32 statusCode = exception switch
            {
                CustomException => (exception as CustomException).StatusCode,
                InvalidOperationException => context.Response.StatusCode,
                _ => context.Response.StatusCode
            };
            ErrorStructure response = new ErrorStructure(exception.Message)
            {
                StatusCode = statusCode,
                Message = message
            };
            
            //response.Details.Add(new DetailError() { Detail =  });

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
