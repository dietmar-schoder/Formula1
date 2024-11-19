using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonRaces(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonRaces.Query, List<SeasonRaceDto>>
{
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
