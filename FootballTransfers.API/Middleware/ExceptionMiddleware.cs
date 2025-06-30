using FootballTransfers.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace FootballTransfers.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ValidationException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new
                    {
                        message = validationEx.Message,
                        errors = validationEx.Errors
                    });
                    break;
                default:
                    result = JsonSerializer.Serialize(new { message = exception.Message });
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new { message = exception.Message });
            }

            await context.Response.WriteAsync(result);
        }
    }
}
