using Formula1.Application.Interfaces.Persistence;
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
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetCircuitByIdQuery, CircuitRacesDto>
{
    public async Task<CircuitRacesDto> Handle(GetCircuitByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var circuit = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .Include(c => c.Races)
            .FirstOrDefaultAsync(c => c.Id.Equals(request.Id), cancellationToken)
            ?? AddNotFoundError<Circuit>(request.Id.ToString());
        if (circuit is null) { return default; }
        Log(circuit.Id.ToString(), nameof(circuit.Id));
        Log(circuit.Races.Count.ToString(), nameof(circuit.Races.Count));
        circuit.Races = [.. circuit.Races.OrderBy(r => r.SeasonYear)];
        return circuit.Adapt<CircuitRacesDto>();
    }
}
