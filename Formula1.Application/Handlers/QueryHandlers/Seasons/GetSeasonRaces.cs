using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonRaces(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonRaces.Query, List<SeasonRaceDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year) : IRequest<List<SeasonRaceDto>> { }

    public async Task<List<SeasonRaceDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Races
            .Where(r => r.SeasonYear == query.Year)
            .Include(s => s.GrandPrix)
            .Include(s => s.Circuit)
            .OrderBy(r => r.Round)
            .Select(r => SeasonRaceDto.FromRace(r))
            .ToListAsync(cancellationToken);
}
