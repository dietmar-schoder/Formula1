using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitsQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetCircuitsQuery, List<CircuitDto>>
{
    public async Task<List<CircuitDto>> Handle(GetCircuitsQuery request, CancellationToken cancellationToken)
    {
        Log();
        var circuits = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
        Log(circuits.Count.ToString(), nameof(circuits.Count));
        return circuits.Adapt<List<CircuitDto>>();
    }
}
