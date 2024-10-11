using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitsQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetCircuitsQuery, List<CircuitBasicDto>>
{
    public async Task<List<CircuitBasicDto>> Handle(GetCircuitsQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var circuits = await _context.FORMULA1_Circuits
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
        _logService.Log(circuits.Count.ToString(), nameof(circuits.Count));
        return circuits.Adapt<List<CircuitBasicDto>>();
    }
}
