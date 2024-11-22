using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Constructors;

public class GetConstructorSeasons(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructorSeasons.Query, List<SeasonDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<List<SeasonDto>> { }

    public async Task<List<SeasonDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.ConstructorId == query.Id)
            .Include(r => r.Session.Race.Season)
            .GroupBy(r => new
            {
                r.Session.Race.SeasonYear,
                SeasonWikipediaUrl = r.Session.Race.Season.WikipediaUrl
            })
            .OrderByDescending(g => g.Key.SeasonYear)
            .Select(g => new SeasonDto
            (
                g.Key.SeasonYear,
                g.Key.SeasonWikipediaUrl
            ))
            .ToListAsync(cancellationToken);
}
