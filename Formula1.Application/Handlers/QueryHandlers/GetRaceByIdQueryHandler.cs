using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetRaceByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetRaceByIdQuery, RaceSessionsDto>
{
    public async Task<RaceSessionsDto> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var race = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(r => r.Season)
            .Include(r => r.Circuit)
            .Include(r => r.Sessions)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            ?? AddNotFoundError<Race>(request.Id.ToString());
        if (race is null) { return default; }
        Log(race.Id.ToString(), nameof(race.Id));
        Log(race.Sessions.Count.ToString(), nameof(race.Sessions.Count));
        race.Sessions = [.. race.Sessions.OrderBy(r => r.SessionTypeId)];
        return race.Adapt<RaceSessionsDto>();
    }
}
