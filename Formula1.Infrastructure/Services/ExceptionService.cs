using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Formula1.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Formula1.Infrastructure.Services;

public class ExceptionService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService,
    ISlackClient slackClient) : IExceptionService
{
    public List<string> Errors { get; } = [];

    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IScopedLogService _logService = logService;
    private readonly ISlackClient _slackClient = slackClient;

    public async Task HandleExceptionInDevelopmentAsync(Exception exception)
    {
        ConsoleWriteError(_logService.ExceptionAsTextBlock(exception));
        await HttpResponseWriteExceptionAsync(new ExceptionResponse(exception.Message, _logService.GetLogsAsList()));
    }

    public async Task HandleExceptionInProductionAsync(Exception exception)
    {
        _slackClient.SendMessage($":boom: EXCEPTION: {_logService.ExceptionAsTextBlock(exception)}");
        await HttpResponseWriteExceptionAsync();
    }

    private async Task HttpResponseWriteExceptionAsync(ExceptionResponse responseBody = default)
    {
        responseBody ??= new ExceptionResponse();
        _httpContext.HttpContext.Response.StatusCode = 500;
        _httpContext.HttpContext.Response.ContentType = "application/json";
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        await _httpContext.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(responseBody, options));
    }

    private static void ConsoleWriteError(string message)
    {
        Debug.WriteLine(string.Empty);
        Debug.WriteLine("== ERROR ==");
        Debug.WriteLine(message);
        Debug.WriteLine("===========");
        Debug.WriteLine(string.Empty);
    }
}
