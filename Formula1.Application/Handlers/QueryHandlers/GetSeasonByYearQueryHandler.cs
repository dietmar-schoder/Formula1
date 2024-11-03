using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonByYearQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSeasonByYearQuery, SeasonRacesDto>
{
    public async Task<SeasonRacesDto> Handle(GetSeasonByYearQuery request, CancellationToken cancellationToken)
    {
        Log(request.Year.ToString(), nameof(request.Year));
        var season = await _dbContext.FORMULA1_Seasons
            .AsNoTracking()
            .Include(s => s.Races).ThenInclude(r => r.Circuit)
            .FirstOrDefaultAsync(s => s.Year == request.Year, cancellationToken)
            ?? AddNotFoundError<Season>(request.Year.ToString());
        if (season is null) { return default; }
        Log(season.Year.ToString(), nameof(season.Year));
        Log(season.Races.Count.ToString(), nameof(season.Races.Count));
        season.Races = [.. season.Races.OrderBy(r => r.Round)];
        return season.Adapt<SeasonRacesDto>();
    }
}
