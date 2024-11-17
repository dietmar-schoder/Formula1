using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSession(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetSession.Query, SessionDto>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

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
        var sessionDto = session.Adapt<SessionDto>();
        sessionDto.SessionTypeDescription = session.SessionType.Description;
        sessionDto.SeasonYear = session.Race.Season.Year;
        sessionDto.Round = session.Race.Round;
        sessionDto.GrandPrixId = session.Race.GrandPrixId;
        sessionDto.GrandPrixName = session.Race.GrandPrix.Name;
        sessionDto.CircuitId = session.Race.CircuitId ?? 0;
        sessionDto.CircuitName = session.Race.Circuit.Name;
        return sessionDto;
    }
}
