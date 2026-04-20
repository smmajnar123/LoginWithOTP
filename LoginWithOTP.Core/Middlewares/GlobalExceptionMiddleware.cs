using LoginWithOTP.DTO.ResModels;
using LoginWithOTP.Shared.Exceptions;
using LoginWithOTP.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LoginWithOTP.Core.Middlewares
{
    public class GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var traceId = context.TraceIdentifier;

            int statusCode = StatusCodes.Status500InternalServerError;
            string message = "An unexpected error occurred";
            string errorCode = "SERVER_ERROR";
            List<ApiError>? errors = null;

            switch (ex)
            {
                case BaseException baseEx:
                    statusCode = baseEx.StatusCode;
                    message = baseEx.Message;
                    errorCode = baseEx.Code;

                    if (baseEx is ValidationException valEx)
                        errors = valEx.Errors;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = "Unauthorized";
                    errorCode = "UNAUTHORIZED";
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = "Resource not found";
                    errorCode = "NOT_FOUND";
                    break;

                default:
                    _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", traceId);
                    break;
            }

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                Errors = errors
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}