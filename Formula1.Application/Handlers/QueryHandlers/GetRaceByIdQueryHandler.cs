﻿using Formula1.Application.Interfaces.Persistence;
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
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetRaceByIdQuery, RaceDto>
{
    public async Task<RaceDto> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var race = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(e => e.Season)
            .Include(e => e.Circuit)
            .Include(e => e.Sessions.OrderBy(s => s.SessionTypeId))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? await ReturnNotFoundErrorAsync<Race>(request.Id.ToString());
        Log(race.Id.ToString(), nameof(race.Id));
        return race.Adapt<RaceDto>();
    }
}
