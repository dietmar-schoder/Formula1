using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessions(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessions.Query, SessionsPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<SessionsPaginatedDto> { }

    public async Task<SessionsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_Sessions.CountAsync(cancellationToken);
        var sessions = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.SessionType)
            .Include(s => s.Race)
                .ThenInclude(r => r.Season)
            .Include(s => s.Race)
                .ThenInclude(r => r.GrandPrix)
            .Include(s => s.Race)
                .ThenInclude(r => r.Circuit)
            .OrderBy(s => s.SessionType)
                .ThenByDescending(s => s.Race.SeasonYear)
                .ThenBy(s => s.Race.Round)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(sessions.Count.ToString(), nameof(sessions.Count));
        return new SessionsPaginatedDto(
            sessions.Adapt<List<SessionDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
