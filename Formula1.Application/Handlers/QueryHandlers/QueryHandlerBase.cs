using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;

namespace Formula1.Application.Handlers.QueryHandlers;

public abstract class HandlerBase(
    IApplicationDbContext context,
    IScopedLogService logService)
{
    protected readonly IApplicationDbContext _context = context;
    protected readonly IScopedLogService _logService = logService;
}
