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
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessionByIdQuery, SessionResultsDto>
{
    public async Task<SessionResultsDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var session = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.Results)
            .Include(s => s.SessionType)
            .Include(s => s.Race).ThenInclude(r => r.Season)
            .Include(s => s.Race).ThenInclude(r => r.Circuit)
            .FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken)
            ?? AddNotFoundError<Session>(request.Id.ToString());
        if (session is null) { return default; }
        Log(session.Id.ToString(), nameof(session.Id));
        Log(session.Results.Count.ToString(), nameof(session.Results.Count));
        session.Results = [.. session.Results.OrderBy(r => r.Position)];
        return session.Adapt<SessionResultsDto>();
    }
}
