using FluentValidation;
using System.Text.Json;

namespace PrivateClinicsNetWebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, 400, "Invalid data provided.", ex.Errors
                    .Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage })
                    .ToList());
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, 500, "An unexpected error occurred.", new[]
                {
                new { Property = "General", ErrorMessage = ex.Message }
            });
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, object errors)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                IsSuccess = false,
                Message = message,
                Errors = errors,
                StatusCode = statusCode,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
