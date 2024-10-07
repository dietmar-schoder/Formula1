using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DbCircuit = Formula1.Domain.Entities.Circuit;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportCircuitsCommandHandler(IApplicationDbContext context, IErgastApisClient ergastApisClient)
    : IRequestHandler<ImportCircuitsCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IErgastApisClient _ergastApisClient = ergastApisClient;

    public async Task<Unit> Handle(ImportCircuitsCommand request, CancellationToken cancellationToken)
    {
        var circuits = await _context.FORMULA1_Circuits.ToDictionaryAsync(e => e.ErgastCircuitId, cancellationToken);
        foreach (var importCircuit in await _ergastApisClient.GetCircuitsAsync())
        {
            circuits.TryGetValue(importCircuit.circuitId, out var existingCircuit);
            UpdateCircuit(existingCircuit ?? await NewCircuit(_context, cancellationToken), importCircuit);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

        static void UpdateCircuit(DbCircuit circuit, Circuit importCircuit)
        {
            circuit.ErgastCircuitId = importCircuit.circuitId;
            circuit.Name = importCircuit.circuitName;
        }

        static async Task<DbCircuit> NewCircuit(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_Circuits.AddAsync(DbCircuit.Create(), cancellationToken)).Entity;
    }
}
