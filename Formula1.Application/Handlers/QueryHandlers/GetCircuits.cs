using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuits(IApplicationDbContext dbContext)
    : IRequestHandler<GetCircuits.Query, CircuitsPaginatedDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<CircuitsPaginatedDto> { }

    public async Task<CircuitsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Circuits.CountAsync(cancellationToken);
        var circuitDtos = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => CircuitDto.FromCircuit(c))
            .ToListAsync(cancellationToken);
        return new CircuitsPaginatedDto(
            circuitDtos,
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
