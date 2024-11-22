using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Sessions;

public class GetSessions(IApplicationDbContext dbContext)
    : IRequestHandler<GetSessions.Query, SessionsPaginatedDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<SessionsPaginatedDto> { }

    public async Task<SessionsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Sessions.CountAsync(cancellationToken);
        var sessionDtos = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.SessionType)
            .Include(s => s.Race)
                .ThenInclude(r => r.Season)
            .Include(s => s.Race)
                .ThenInclude(r => r.GrandPrix)
            .Include(s => s.Race)
                .ThenInclude(r => r.Circuit)
            .OrderByDescending(s => s.Race.SeasonYear)
                .ThenBy(s => s.Race.Round)
                .ThenBy(s => s.SessionType)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => SessionDto.FromSession(s))
            .ToListAsync(cancellationToken);
        return new SessionsPaginatedDto(
            sessionDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
