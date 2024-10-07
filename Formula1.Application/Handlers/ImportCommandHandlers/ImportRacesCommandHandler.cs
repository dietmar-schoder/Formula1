using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DbRace = Formula1.Domain.Entities.Race;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportRacesCommandHandler(
    IApplicationDbContext context,
    IErgastApisClient ergastApisClient,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportRacesCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IErgastApisClient _ergastApisClient = ergastApisClient;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Unit> Handle(ImportRacesCommand request, CancellationToken cancellationToken)
    {
        var circuits = await _context.FORMULA1_Circuits.AsNoTracking().ToDictionaryAsync(e => e.ErgastCircuitId, e => e.Id, cancellationToken);

        await _dateTimeProvider.ForAllYears(request.FromYear, request.ToYear, async year =>
        {
            var races = await _context.FORMULA1_Races.Where(r => r.SeasonYear.Equals(year)).ToDictionaryAsync(e => e.Round, cancellationToken);
            var importRaces = await _ergastApisClient.GetRacesAsync(year);
            await UpdateInsertRaces(circuits, races, importRaces, year, cancellationToken);
        });

        return Unit.Value;

        async Task UpdateInsertRaces(
            Dictionary<string, Guid> circuits,
            Dictionary<int, DbRace> races,
            List<Race> importRaces,
            int year,
            CancellationToken cancellationToken)
        {
            foreach (var importRace in importRaces)
            {
                var round = int.Parse(importRace.round);
                races.TryGetValue(round, out var existingRace);
                UpdateRace(circuits, existingRace ?? await InsertRace(_context, cancellationToken), importRace, round);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        void UpdateRace(Dictionary<string, Guid> circuits, DbRace race, Race importRace, int round)
        {
            race.SeasonYear = int.Parse(importRace.season);
            race.Round = round;
            race.CircuitId = circuits[importRace.Circuit.circuitId];
        }

        async Task<DbRace> InsertRace(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_Races.AddAsync(DbRace.Create(), cancellationToken)).Entity;
    }
}
