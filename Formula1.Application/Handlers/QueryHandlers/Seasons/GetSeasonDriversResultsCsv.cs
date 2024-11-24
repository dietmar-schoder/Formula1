using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonDriversResultsCsv(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonDriversResultsCsv.Query, string>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year) : IRequest<string> { }

    public async Task<string> Handle(Query query, CancellationToken cancellationToken)
    {
        var csv = new List<string>();
        var results = await _dbContext.FORMULA1_Results
            .Where(r => r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Driver)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenByDescending(r => r.Session.SessionTypeId)
            .Select(r => SeasonDriverResultBasicDto.FromResult(r))
            .ToListAsync(cancellationToken);

        Dictionary<int, int> distinctSessionIds = results
            .DistinctBy(r => r.SessionId)
            .Select((r, index) => new { r.SessionId, Index = index })
            .ToDictionary(x => x.SessionId, x => x.Index);

        var driversResults = new Dictionary<string, string[]>();
        foreach (var result in results)
        {
            driversResults.TryGetValue(result.DriverName, out var driverRow);
            if (driverRow is null)
            {
                driverRow = new string[distinctSessionIds.Count];
                driversResults.Add(result.DriverName, driverRow);
            }
            var index = distinctSessionIds[result.SessionId];
            driverRow[index] = result.Position.ToString();
        }

        csv.Add($"\"{query.Year}\" Positions," + string.Join(',', distinctSessionIds.Select(i => i.Value + 1)));
        foreach (var driversResult in driversResults)
        {
            csv.Add($"\"{driversResult.Key}\"," + string.Join(',', driversResult.Value.Select(i => i)));
        }
        return string.Join(Environment.NewLine, csv);
    }
}
