using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Constructors;

public class GetConstructorRaces(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructorRaces.Query, RacesPaginatedDto<RaceDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<RacesPaginatedDto<RaceDto>> { }

    public async Task<RacesPaginatedDto<RaceDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.ConstructorId == query.Id)
            .GroupBy(r => r.Session.Race)
            .CountAsync(cancellationToken);
        var driverDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.ConstructorId == query.Id)
            .Include(r => r.Session.Race.Season)
            .Include(r => r.Session.Race.GrandPrix)
            .Include(r => r.Session.Race.Circuit)
            .GroupBy(r => new
            {
                RaceId = r.Session.Race.Id,
                r.Session.Race.SeasonYear,
                SeasonWikipediaUrl = r.Session.Race.Season.WikipediaUrl,
                r.Session.Race.Round,
                r.Session.Race.WikipediaUrl,
                r.Session.Race.GrandPrixId,
                GrandPrixName = r.Session.Race.GrandPrix.Name,
                GrandPrixWikipediaUrl = r.Session.Race.GrandPrix.WikipediaUrl,
                CircuitId = r.Session.Race.Circuit.Id,
                CircuitName = r.Session.Race.Circuit.Name,
                CircuitWikipediaUrl = r.Session.Race.Circuit.WikipediaUrl
            })
            .OrderByDescending(g => g.Key.SeasonYear)
                .ThenBy(g => g.Key.Round)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new RaceDto(
                g.Key.RaceId,
                g.Key.SeasonYear,
                g.Key.SeasonWikipediaUrl,
                g.Key.Round,
                g.Key.WikipediaUrl,
                g.Key.GrandPrixId,
                g.Key.GrandPrixName,
                g.Key.GrandPrixWikipediaUrl,
                g.Key.CircuitId,
                g.Key.CircuitName,
                g.Key.CircuitWikipediaUrl))
            .ToListAsync(cancellationToken);
        return new RacesPaginatedDto<RaceDto>(
            driverDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
