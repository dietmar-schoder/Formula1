using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Runtime.CompilerServices;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonConstructorsResultsCsv(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonConstructorsResultsCsv.Query, string>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private record PositionNumber{
        public int Position { get; set; }
        public int Number { get; set; }
    }
    public record Query(int Year) : IRequest<string> { }

    public async Task<string> Handle(Query query, CancellationToken cancellationToken)
    {
        var csv = new List<string>();
        var results = await _dbContext.FORMULA1_Results
            .Where(r => r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Constructor)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenByDescending(r => r.Session.SessionTypeId)
            .Select(r => SeasonConstructorResultBasicDto.FromResult(r))
            .ToListAsync(cancellationToken);

        Dictionary<int, int> distinctSessionIds = results
            .DistinctBy(r => r.SessionId)
            .Select((r, index) => new { r.SessionId, Index = index })
            .ToDictionary(x => x.SessionId, x => x.Index);

        var driversResults = new Dictionary<string, PositionNumber[]>();
        foreach (var result in results)
        {
            driversResults.TryGetValue(result.ConstructorName, out var driverRow);
            if (driverRow is null)
            {
                driverRow = new PositionNumber[distinctSessionIds.Count];
                driversResults.Add(result.ConstructorName, driverRow);
            }
            var index = distinctSessionIds[result.SessionId];
            var driverRowCell = driverRow[index];
            if (driverRowCell is null)
            {
                driverRow[index] = new PositionNumber();
            }
            driverRow[index].Position += result.Position;
            driverRow[index].Number += 1;
        }

        csv.Add($"\"{query.Year}\" Positions," + string.Join(',', distinctSessionIds.Select(i => i.Value + 1)));
        foreach (var driversResult in driversResults)
        {
            csv.Add($"\"{driversResult.Key}\"," + string.Join(',',
                driversResult.Value.Select(i => i is not null && i.Number > 0 ? (i.Position * 1m / i.Number).ToString() : string.Empty)));
        }
        return string.Join(Environment.NewLine, csv);
    }
}
