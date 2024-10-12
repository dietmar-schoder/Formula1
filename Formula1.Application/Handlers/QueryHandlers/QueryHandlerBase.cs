using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Domain.Exceptions;

namespace Formula1.Application.Handlers.QueryHandlers;

public abstract class HandlerBase(
    IApplicationDbContext context,
    IScopedLogService logService)
{
    protected readonly IApplicationDbContext _context = context;
    protected readonly IScopedLogService _logService = logService;

    protected void Log(string content, string var = "")
        => _logService.Log(content, var);

    protected static void ThrowError(int statusCode, string message)
        => throw new UserException(statusCode, message);

    protected static T ThrowNotFoundError<T>(string key) where T : class
        => throw new UserException(404, $"Resource {typeof(T).Name} not found for '{key}'.");

    protected static void ThrowException()
        => throw new Exception();
}
