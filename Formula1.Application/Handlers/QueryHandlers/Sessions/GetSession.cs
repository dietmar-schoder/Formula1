using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Sessions;

public class GetSession(IApplicationDbContext dbContext)
    : IRequestHandler<GetSession.Query, SessionDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<SessionDto> { }

    public async Task<SessionDto> Handle(Query request, CancellationToken cancellationToken)
    {
        var session = await _dbContext.FORMULA1_Sessions
            .AsNoTracking()
            .Include(s => s.SessionType)
            .Include(s => s.Race)
                .ThenInclude(r => r.Season)
            .Include(s => s.Race)
                .ThenInclude(r => r.GrandPrix)
            .Include(s => s.Race)
                .ThenInclude(r => r.Circuit)
            .FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
        if (session is null) { return null; }
        return SessionDto.FromSession(session);
    }
}
