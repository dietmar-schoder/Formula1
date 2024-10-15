using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace Formula1.Application.Handlers.QueryHandlers;

public abstract class HandlerBase(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
{
    protected readonly IApplicationDbContext _dbContext = dbContext;
    protected readonly IScopedLogService _logService = logService;
    protected readonly IScopedErrorService _errorService = errorService;

    protected void Log(
        string content = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0)
        => _logService.Log(content, var, callerMethod, callerFile, callerLine);

    protected void AddError(string message)
        => _errorService.AddError(message);

    protected void AddErrorIf(bool condition, string message)
        => _errorService.AddErrorIf(condition, message);

    protected T AddNotFoundError<T>(string key) where T : class
        => _errorService.AddNotFoundError<T>(key);
}
