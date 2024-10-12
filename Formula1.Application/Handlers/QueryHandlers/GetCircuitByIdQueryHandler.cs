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
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetCircuitByIdQuery, CircuitDto>
{
    public async Task<CircuitDto> Handle(GetCircuitByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        //var zero = 0;
        //var y = 1 / zero;
        var circuit = await _context.FORMULA1_Circuits
            .AsNoTracking()
            .Include(e => e.Races.OrderBy(r => r.SeasonYear))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? ThrowNotFoundError<Circuit>(request.Id.ToString());
        Log(circuit.Id.ToString(), nameof(circuit.Id));
        return circuit.Adapt<CircuitDto>();
    }
}
