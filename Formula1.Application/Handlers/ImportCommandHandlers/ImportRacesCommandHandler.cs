using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DbRace = Formula1.Domain.Entities.Race;
using DbCircuit = Formula1.Domain.Entities.Circuit;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportRacesCommandHandler(IApplicationDbContext context, IErgastApisClient ergastApisClient)
    : IRequestHandler<ImportRacesCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IErgastApisClient _ergastApisClient = ergastApisClient;

    public async Task<Unit> Handle(ImportRacesCommand request, CancellationToken cancellationToken)
    {
        var dbCircuits = await _context.FORMULA1_Circuits.AsNoTracking().ToDictionaryAsync(e => e.ErgastCircuitId, e => e.Id, cancellationToken);
        var fromYear = request.Year > 0 ? request.Year : 1950;
        var toYear = request.Year > 0 ? request.Year : 2024;

        for (int year = fromYear; year <= toYear; year++)
        {
            var dbRaces = await _context.FORMULA1_Races.Where(r => r.SeasonYear.Equals(year)).ToDictionaryAsync(e => e.Round, cancellationToken);
            foreach (var importRace in await _ergastApisClient.GetRacesAsync(year))
            {
                var round = int.Parse(importRace.round);
                dbRaces.TryGetValue(round, out var existingRace);
                UpdateDbRace(dbCircuits, existingRace ?? (await _context.FORMULA1_Races.AddAsync(new DbRace(Guid.NewGuid()), cancellationToken)).Entity, importRace, round);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }

    private static void UpdateDbRace(Dictionary<string, Guid> dbCircuits , DbRace dbRace, Race importRace, int round)
    {
        dbRace.SeasonYear = int.Parse(importRace.season);
        dbRace.Round = round;
        dbRace.CircuitId = dbCircuits[importRace.Circuit.circuitId];
    }
}
