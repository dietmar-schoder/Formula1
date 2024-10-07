using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.F1Dtos;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using Formula1.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportConstructorsCommandHandler(
    IApplicationDbContext context,
    IF1Client formula1Client,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportConstructorsCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IF1Client _formula1Client = formula1Client;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Unit> Handle(ImportConstructorsCommand request, CancellationToken cancellationToken)
    {
        await _dateTimeProvider.ForAllYears(request.FromYear, request.ToYear, async year =>
        {
            var constructors = await _context.FORMULA1_Constructors.ToDictionaryAsync(e => e.Name, cancellationToken);
            var importConstructors = await _formula1Client.GetConstructorsAsync(year);
            await UpdateInsertConstructors(constructors, importConstructors, year, cancellationToken);
        });

        return Unit.Value;

        async Task UpdateInsertConstructors(
            Dictionary<string, Constructor> constructors,
            List<F1Constructor> importConstructors,
            int year,
            CancellationToken cancellationToken)
        {
            foreach (var importConstructor in importConstructors)
            {
                constructors.TryGetValue(importConstructor.Name, out var existingConstructor);
                UpdateConstructor(existingConstructor ?? await InsertConstructor(_context, cancellationToken), importConstructor);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        void UpdateConstructor(Constructor constructor, F1Constructor importConstructor)
            => constructor.Name = importConstructor.Name;

        async Task<Constructor> InsertConstructor(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_Constructors.AddAsync(Constructor.Create(), cancellationToken)).Entity;
    }
}
