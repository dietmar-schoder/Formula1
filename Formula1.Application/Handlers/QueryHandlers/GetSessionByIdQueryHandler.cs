using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionByIdQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetSessionByIdQuery, SessionDto>
{
    public async Task<SessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        _logService.Log(request.Id.ToString(), nameof(request.Id));
        var session = await _context.FORMULA1_Sessions
            .AsNoTracking()
            .Include(e => e.Results)
            .Include(e => e.SessionType)
            .Include(e => e.Race)
            .ThenInclude(e => e.Circuit)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new Exception("404");
        _logService.Log(session.Id.ToString(), nameof(session.Id));
        return session.Adapt<SessionDto>();
    }
}
