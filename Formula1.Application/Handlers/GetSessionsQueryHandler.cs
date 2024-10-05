using Formula1.Application.Interfaces;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers;

public class GetSessionsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSessionsQuery, List<SessionDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<SessionDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Sessions
            .AsNoTracking()
            .Include(e => e.SessionType)
            .Include(e => e.Race)
            .ThenInclude(e => e.Circuit)
            .ToListAsync(cancellationToken))
            .Adapt<List<SessionDto>>();
}
