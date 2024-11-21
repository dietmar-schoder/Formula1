using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuit(IApplicationDbContext dbContext)
    : IRequestHandler<GetCircuit.Query, CircuitDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<CircuitDto> { }

    public async Task<CircuitDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var circuit = await _dbContext.FORMULA1_Circuits
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id.Equals(query.Id), cancellationToken);
        return circuit is null ? null : CircuitDto.FromCircuit(circuit);
    }
}
