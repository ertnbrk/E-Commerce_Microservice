using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace SharedKernel.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ErrorResponse
            {
                Message = exception.Message,
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.Status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
