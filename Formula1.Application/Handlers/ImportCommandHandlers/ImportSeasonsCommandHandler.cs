using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DbSeason = Formula1.Domain.Entities.Season;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportSeasonsCommandHandler(IApplicationDbContext context, IErgastApisClient ergastApisClient)
    : IRequestHandler<ImportSeasonsCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IErgastApisClient _ergastApisClient = ergastApisClient;

    public async Task<Unit> Handle(ImportSeasonsCommand request, CancellationToken cancellationToken)
    {
        var seasons = await _context.FORMULA1_Seasons.ToDictionaryAsync(e => e.Year, cancellationToken);
        foreach (var importSeason in await _ergastApisClient.GetSeasonsAsync())
        {
            var year = int.Parse(importSeason.season);
            seasons.TryGetValue(year, out var existingSeason);
            UpdateSeason(existingSeason ?? await NewSeason(context, cancellationToken, year), importSeason);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

        static void UpdateSeason(DbSeason season, Season importSeason)
            => season.WikipediaUrl = importSeason.url;

        static async Task<DbSeason> NewSeason(IApplicationDbContext context, CancellationToken cancellationToken, int year)
            => (await context.FORMULA1_Seasons.AddAsync(DbSeason.Create(year), cancellationToken)).Entity;
    }
}
