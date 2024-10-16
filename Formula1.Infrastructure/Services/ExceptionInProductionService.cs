using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Formula1.Infrastructure.Services;

public class ExceptionInProductionService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService,
    ISlackClient slackClient)
    : IExceptionService
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IScopedLogService _logService = logService;
    private readonly ISlackClient _slackClient = slackClient;

    public async Task HandleExceptionAsync(Exception exception)
    {
        _httpContext.HttpContext.Response.StatusCode = 500;
        _httpContext.HttpContext.Response.ContentType = "application/json";
        await _httpContext.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new { }));

        _slackClient.SendMessage($":boom: EXCEPTION: {_logService.ExceptionAsTextBlock(exception)}");
    }
}
