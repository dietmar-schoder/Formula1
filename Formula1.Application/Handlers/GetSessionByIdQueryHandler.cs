using Formula1.Application.Interfaces;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers;

public class GetSessionByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSessionByIdQuery, SessionDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<SessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Sessions
            .AsNoTracking()
            .Include(e => e.Results)
            .Include(e => e.SessionType)
            .Include(e => e.Race)
            .ThenInclude(e => e.Circuit)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken))?
            .Adapt<SessionDto>();
}
