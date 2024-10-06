using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DbSeason = Formula1.Domain.Entities.Season;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportRacesCommandHandler(IApplicationDbContext context, IErgastApisClient ergastApisClient)
    : IRequestHandler<ImportSeasonsCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IErgastApisClient _ergastApisClient = ergastApisClient;

    public async Task<Unit> Handle(ImportSeasonsCommand request, CancellationToken cancellationToken)
    {
        var dbSeasons = await _context.FORMULA1_Seasons.ToDictionaryAsync(e => e.Year, cancellationToken);
        foreach (var season in await _ergastApisClient.GetSeasonsAsync())
        {
            var year = int.Parse(season.season);
            dbSeasons.TryGetValue(year, out var existingEntity);
            UpdateDbSeason(existingEntity ?? (await _context.FORMULA1_Seasons.AddAsync(new DbSeason(year), cancellationToken)).Entity, season);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

        static void UpdateDbSeason(DbSeason dbSeason, Season season)
        {
            dbSeason.WikipediaUrl = season.url;
        }
    }
}
