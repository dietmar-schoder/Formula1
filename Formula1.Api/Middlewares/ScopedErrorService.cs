using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Formula1.Contracts.Responses;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Formula1.Api.Middlewares;

public class ScopedErrorService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService,
    ISlackClient slackClient) : IScopedErrorService
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IScopedLogService _logService = logService;
    private readonly ISlackClient _slackClient = slackClient;
    private readonly List<string> _errors = [];

    public void AddError(string message)
        => _errors.Add(message);

    public void AddErrorIf(bool condition, string message)
    {
        if (condition) { AddError(message); }
    }

    public async Task ReturnErrorsIfAny(int statusCode = 400)
    {
        if (_errors.Count > 0) { await HttpResponseWriteAsync(statusCode, _errors); }
    }

    public async Task ReturnError(string message, int statusCode = 400)
    {
        AddError(message);
        await ReturnErrorsIfAny(statusCode);
    }

    public async Task<T> ReturnNotFoundErrorAsync<T>(string key) where T : class
    {
        await ReturnError($"Resource {typeof(T).Name} not found for '{key}'.", 404);
        return default;
    }

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

    private async Task HttpResponseWriteAsync<T>(int statusCode, T responseBody)
    {
        _httpContext.HttpContext.Response.StatusCode = statusCode;
        _httpContext.HttpContext.Response.ContentType = "application/json";
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        await _httpContext.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(responseBody, options));
    }

    private async Task HttpResponseWriteExceptionAsync(ExceptionResponse exceptionResponse = default)
        => await HttpResponseWriteAsync(500, exceptionResponse ?? new ExceptionResponse());

    private static void ConsoleWriteError(string message)
    {
        Debug.WriteLine(string.Empty);
        Debug.WriteLine("== ERROR ==");
        Debug.WriteLine(message);
        Debug.WriteLine("===========");
        Debug.WriteLine(string.Empty);
    }
}
