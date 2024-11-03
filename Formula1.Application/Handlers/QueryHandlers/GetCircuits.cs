using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuits(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetCircuits.Query, CircuitsPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<CircuitsPaginatedDto> { }

    public async Task<CircuitsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_Circuits.CountAsync(cancellationToken);
        var circuits = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(circuits.Count.ToString(), nameof(circuits.Count));
        return new CircuitsPaginatedDto(
            circuits.Adapt<List<CircuitDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
