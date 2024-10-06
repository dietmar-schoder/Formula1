using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionTypesQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSessionTypesQuery, List<SessionTypeDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<SessionTypeDto>> Handle(GetSessionTypesQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_SessionTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken))
            .Adapt<List<SessionTypeDto>>();
}
