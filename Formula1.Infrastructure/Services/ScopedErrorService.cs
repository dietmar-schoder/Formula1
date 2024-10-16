using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Microsoft.AspNetCore.Http;

namespace Formula1.Infrastructure.Services;

public class ScopedErrorService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService,
    ISlackClient slackClient) : IScopedErrorService
{
    public List<string> Errors { get; } = [];

    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IScopedLogService _logService = logService;
    private readonly ISlackClient _slackClient = slackClient;

    public void AddError(string message)
        => Errors.Add(message);

    public void AddErrorIf(bool condition, string message)
    {
        if (condition) { AddError(message); }
    }

    public T AddNotFoundError<T>(string key) where T : class
    {
        AddError($"Resource {typeof(T).Name} not found for '{key}'.");
        return default;
    }
}
