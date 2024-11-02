using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetRacesQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetRacesQuery, List<RaceBasicDto>>
{
    public async Task<List<RaceBasicDto>> Handle(GetRacesQuery request, CancellationToken cancellationToken)
    {
        Log();
        var races = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(r => r.Season)
            .Include(r => r.GrandPrix)
            .Include(r => r.Circuit)
            .OrderByDescending(e => e.SeasonYear)
                .ThenBy(r => r.Round)
            .ToListAsync(cancellationToken);
        Log(races.Count.ToString(), nameof(races.Count));
        return races.Adapt<List<RaceBasicDto>>();
    }
}
