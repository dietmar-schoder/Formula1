using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetConstructorsQuery, List<ConstructorBasicDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<ConstructorBasicDto>> Handle(GetConstructorsQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Constructors
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken))
            .Adapt<List<ConstructorBasicDto>>();
}
