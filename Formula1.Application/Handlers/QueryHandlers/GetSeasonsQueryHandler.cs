using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonsQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSeasonsQuery, List<SeasonRacesDto>>
{
    public async Task<List<SeasonRacesDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        Log();
        var seasons = await _dbContext.FORMULA1_Seasons
            .AsNoTracking()
            .OrderByDescending(e => e.Year)
            .ToListAsync(cancellationToken);
        Log(seasons.Count.ToString(), nameof(seasons.Count));
        return seasons.Adapt<List<SeasonRacesDto>>();
    }
}
