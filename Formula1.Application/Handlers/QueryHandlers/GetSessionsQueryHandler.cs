using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionsQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetSessionsQuery, List<SessionDto>>
{
    public async Task<List<SessionDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var sessions = await _context.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.SessionType)
            .Include(s => s.Race)
            .ThenInclude(r => r.Circuit)
            .OrderBy(s => s.SessionType)
            .ThenBy(s => s.Race.Circuit.Name)
            .ToListAsync(cancellationToken);
        _logService.Log(sessions.Count.ToString(), nameof(sessions.Count));
        return sessions.Adapt<List<SessionDto>>();
    }
}
