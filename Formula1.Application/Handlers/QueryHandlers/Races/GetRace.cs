using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Races;

public class GetRace(IApplicationDbContext dbContext)
    : IRequestHandler<GetRace.Query, RaceDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<RaceDto> { }

    public async Task<RaceDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var race = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(r => r.Season)
            .Include(r => r.GrandPrix)
            .Include(r => r.Circuit)
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);
        return race is null ? null : RaceDto.FromRace(race);
    }
}
