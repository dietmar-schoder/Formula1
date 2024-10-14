﻿using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetCircuitByIdQuery, CircuitDto>
{
    public async Task<CircuitDto> Handle(GetCircuitByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var circuit = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .Include(e => e.Races.OrderBy(r => r.SeasonYear))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? await ReturnNotFoundErrorAsync<Circuit>(request.Id.ToString());
        Log(circuit.Id.ToString(), nameof(circuit.Id));
        return circuit.Adapt<CircuitDto>();
    }
}
