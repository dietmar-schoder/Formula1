using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;

namespace Formula1.Infrastructure.Services;

public class ExceptionInDevelopmentService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService)
    : IExceptionService
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IScopedLogService _logService = logService;

    public async Task HandleExceptionAsync(Exception exception)
    {
        _httpContext.HttpContext.Response.StatusCode = 500;
        _httpContext.HttpContext.Response.ContentType = "application/json";
        var responseBody = new ExceptionResponse(exception.Message, _logService.GetLogsAsList());
        await _httpContext.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(responseBody));

        Debug.WriteLine(string.Empty);
        Debug.WriteLine("== ERROR ==");
        Debug.WriteLine(_logService.ExceptionAsTextBlock(exception));
        Debug.WriteLine("===========");
        Debug.WriteLine(string.Empty);
    }
}
