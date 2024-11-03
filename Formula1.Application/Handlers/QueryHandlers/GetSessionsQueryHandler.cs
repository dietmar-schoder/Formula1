using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionsQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessionsQuery, List<SessionDto>>
{
    public async Task<List<SessionDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        Log();
        var sessions = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.SessionType)
            .Include(s => s.Race)
            .ThenInclude(r => r.Season)
            .Include(s => s.Race)
            .ThenInclude(r => r.Circuit)
            .OrderBy(s => s.SessionType)
            .ThenByDescending(s => s.Race.SeasonYear)
            .ThenBy(s => s.Race.Round)
            .ToListAsync(cancellationToken);
        Log(sessions.Count.ToString(), nameof(sessions.Count));
        return sessions.Adapt<List<SessionDto>>();
    }
}
