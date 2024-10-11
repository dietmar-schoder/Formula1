using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
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
        _logService.Log(request.Id.ToString(), nameof(request.Id));
        var circuit = await _context.FORMULA1_Circuits
            .AsNoTracking()
            .Include(e => e.Races.OrderBy(r => r.SeasonYear))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new Exception("404");
        _logService.Log(circuit.Id.ToString(), nameof(circuit.Id));
        return circuit.Adapt<CircuitDto>();
    }
}
