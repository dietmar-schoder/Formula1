using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace Formula1.Application.Handlers.QueryHandlers;

public abstract class HandlerBase(
    IApplicationDbContext context,
    IScopedLogService logService)
{
    protected readonly IApplicationDbContext _context = context;
    protected readonly IScopedLogService _logService = logService;

    protected void Log(
        string content = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0)
        => _logService.Log(content, var, callerMethod, callerFile, callerLine);

    protected void ThrowError(int statusCode, string message)
        => _logService.ThrowError(statusCode, message);

    protected T ThrowNotFoundError<T>(string key) where T : class
        => _logService.ThrowNotFoundError<T>(key);

    protected void ThrowException()
        => _logService.ThrowException();
}
