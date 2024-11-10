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
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessionByIdQuery, SessionDto>
{
    public async Task<SessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var session = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken)
            ?? AddNotFoundError<Session>(request.Id.ToString());
        if (session is null) { return default; }
        Log(session.Id.ToString(), nameof(session.Id));
        return session.Adapt<SessionDto>();
    }
}
