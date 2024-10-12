using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Responses;
using Formula1.Domain.Exceptions;
using System.Diagnostics;
using System.Text.Json;

namespace Formula1.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IScopedLogService logService)
    {
        try
        {
            await _next(context);
        }
        catch (UserException ex)
        {
            await WriteErrorResponseAsync(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine($"==> Exception: {ex.Message}");
            var logs = logService.GetLogs();
            for (int i = 0; i < logs.Count; i++)
            {
                Debug.WriteLine($"==> {i + 1}. {logs[i]}");
            }
            Debug.WriteLine(string.Empty);

            await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, ex.Message);
        }

        static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string errorMessage)
        {
            var body = string.IsNullOrEmpty(errorMessage) ? string.Empty : JsonSerializer.Serialize(new ErrorResponse(errorMessage));
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(body);
        }
    }
}
