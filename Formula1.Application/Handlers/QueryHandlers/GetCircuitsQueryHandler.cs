using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCircuitsQuery, List<CircuitDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<CircuitDto>> Handle(GetCircuitsQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Circuits
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken))
            .Adapt<List<CircuitDto>>();
}
