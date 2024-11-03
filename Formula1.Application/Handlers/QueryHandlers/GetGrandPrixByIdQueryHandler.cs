using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetGrandPrixByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetGrandPrixByIdQuery, GrandPrixRacesDto>
{
    public async Task<GrandPrixRacesDto> Handle(GetGrandPrixByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var grandPrix = await _dbContext.FORMULA1_GrandPrix
            .AsNoTracking()
            .Include(g => g.Races)
            .FirstOrDefaultAsync(g => g.Id.Equals(request.Id), cancellationToken)
            ?? AddNotFoundError<GrandPrix>(request.Id.ToString());
        if (grandPrix is null) { return default; }
        Log(grandPrix.Id.ToString(), nameof(grandPrix.Id));
        Log(grandPrix.Races.Count.ToString(), nameof(grandPrix.Races.Count));
        grandPrix.Races = [.. grandPrix.Races.OrderBy(r => r.SeasonYear)];
        return grandPrix.Adapt<GrandPrixRacesDto>();
    }
}
