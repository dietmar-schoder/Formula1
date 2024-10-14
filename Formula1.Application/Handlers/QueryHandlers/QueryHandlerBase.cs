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

    public async Task ReturnErrorsIfAny()
        => await _errorService.ReturnErrorsIfAny();

    public async Task ReturnError(string message, int statusCode = 400)
        => await _errorService.ReturnError(message, statusCode);

    protected async Task<T> ReturnNotFoundErrorAsync<T>(string key) where T : class
        => await _errorService.ReturnNotFoundErrorAsync<T>(key);
}
