using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.F1Dtos;
using Formula1.Contracts.Responses;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using Formula1.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace Formula1.Application.Handlers.ImportCommandHandlers;

public class ImportGrandPrixCommandHandler(
    IApplicationDbContext context,
    IF1Client formula1Client,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportGrandPrixCommand, ImportResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IF1Client _formula1Client = formula1Client;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<ImportResponse> Handle(ImportGrandPrixCommand request, CancellationToken cancellationToken)
    {
        var response = new ImportResponse(request.GetType().Name, request.FromYear, request.ToYear);
        var grandPrix = await _context.FORMULA1_GrandPrix.ToDictionaryAsync(e => e.Name, cancellationToken);
        var importGrandPrix = new Dictionary<string, F1GrandPrix>();

        response.RowsInDatabase += grandPrix.Count;
        await _dateTimeProvider.ForAllYears(request.FromYear, request.ToYear, async year =>
        {
            foreach (var grandPrix in await _formula1Client.GetGrandPrixAsync(year))
            {
                importGrandPrix.TryAdd(grandPrix.Name, grandPrix);
            }
        });
        response.UniqueRowsInImport = importGrandPrix.Count;
        return await UpdateInsertGrandPrix(grandPrix, importGrandPrix, response, cancellationToken);

        async Task<ImportResponse> UpdateInsertGrandPrix(
            Dictionary<string, GrandPrix> grandPrix,
            Dictionary<string, F1GrandPrix> importGrandPrix,
            ImportResponse reponse,
            CancellationToken cancellationToken)
        {
            var rowsInserted = 0;
            foreach (var importGrandPrixEntry in importGrandPrix)
            {
                var importGrandPrixObject = importGrandPrixEntry.Value;
                importGrandPrixObject.Name = FormatGrandPrixName(importGrandPrixObject.Name);
                grandPrix.TryGetValue(importGrandPrixObject.Name, out var existingGrandPrix);
                rowsInserted += existingGrandPrix is null ? 1 : 0;
                UpdateGrandPrix(existingGrandPrix ?? await InsertGrandPrix(_context, cancellationToken), importGrandPrixObject);
            }
            response.RowsInserted += rowsInserted;
            response.RowsUpdated += await _context.SaveChangesAsync(cancellationToken) - rowsInserted;
            return response;
        }

        void UpdateGrandPrix(GrandPrix grandPrix, F1GrandPrix importGrandPrix)
            => grandPrix.Name = importGrandPrix.Name;

        async Task<GrandPrix> InsertGrandPrix(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_GrandPrix.AddAsync(GrandPrix.Create(), cancellationToken)).Entity;

        string FormatGrandPrixName(string name)
            => Regex.Replace(WebUtility.HtmlDecode(name).Trim(), @"\s+", " ");
    }
}
