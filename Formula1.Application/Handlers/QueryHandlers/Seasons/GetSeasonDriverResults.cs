using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonDriverResults(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonDriverResults.Query, List<SeasonDriverResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year, int DriverId) : IRequest<List<SeasonDriverResultDto>> { }

    public async Task<List<SeasonDriverResultDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Results
            .Where(r => r.DriverId == query.DriverId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Constructor)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Position)
            .Select(r => SeasonDriverResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
}
