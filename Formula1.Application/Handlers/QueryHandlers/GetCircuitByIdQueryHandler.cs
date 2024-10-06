using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCircuitByIdQuery, CircuitDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CircuitDto> Handle(GetCircuitByIdQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Circuits
            .AsNoTracking()
            .Include(e => e.Races)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken))?
            .Adapt<CircuitDto>();
}
