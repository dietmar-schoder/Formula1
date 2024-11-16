using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetRaces(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetRaces.Query, RacesPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<RacesPaginatedDto> { }

    public async Task<RacesPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Races.CountAsync(cancellationToken);
        var races = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(r => r.Season)
            .Include(r => r.GrandPrix)
            .Include(r => r.Circuit)
            .OrderByDescending(e => e.SeasonYear)
                .ThenBy(r => r.Round)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new RacesPaginatedDto(
            races.Adapt<List<RaceDto>>(),
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
