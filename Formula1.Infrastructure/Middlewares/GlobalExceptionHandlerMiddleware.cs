using Formula1.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Formula1.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IScopedLogService logService)
    {
        try
        {
            await _next(context);
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentLength = 0;
        }
    }
}
