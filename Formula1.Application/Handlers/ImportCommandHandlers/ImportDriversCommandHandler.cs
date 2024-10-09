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

public class ImportDriversCommandHandler(
    IApplicationDbContext context,
    IF1Client formula1Client,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportDriversCommand, ImportResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IF1Client _formula1Client = formula1Client;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<ImportResponse> Handle(ImportDriversCommand request, CancellationToken cancellationToken)
    {
        var response = new ImportResponse(request.GetType().Name, request.FromYear, request.ToYear);
        var drivers = await _context.FORMULA1_Drivers.ToDictionaryAsync(e => e.Name, cancellationToken);
        var importDrivers = new Dictionary<string, F1Driver>();

        response.RowsInDatabase += drivers.Count;
        await _dateTimeProvider.ForAllYears(request.FromYear, request.ToYear, async year =>
        {
            foreach (var driver in await _formula1Client.GetDriversAsync(year))
            {
                importDrivers.TryAdd(driver.Name, driver);
            }
        });
        response.UniqueRowsInImport = importDrivers.Count;
        return await UpdateInsertDrivers(drivers, importDrivers, response, cancellationToken);

        async Task<ImportResponse> UpdateInsertDrivers(
            Dictionary<string, Driver> drivers,
            Dictionary<string, F1Driver> importDrivers,
            ImportResponse reponse,
            CancellationToken cancellationToken)
        {
            var rowsInserted = 0;
            foreach (var importDriverEntry in importDrivers)
            {
                var importDriver = importDriverEntry.Value;
                importDriver.Name = FormatDriverName(importDriver.Name);
                drivers.TryGetValue(importDriver.Name, out var existingDriver);
                rowsInserted += existingDriver is null ? 1 : 0;
                UpdateDriver(existingDriver ?? await InsertDriver(_context, cancellationToken), importDriver);
            }
            response.RowsInserted += rowsInserted;
            response.RowsUpdated += await _context.SaveChangesAsync(cancellationToken) - rowsInserted;
            return response;
        }

        void UpdateDriver(Driver driver, F1Driver importDriver)
            => driver.Name = importDriver.Name;

        async Task<Driver> InsertDriver(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_Drivers.AddAsync(Driver.Create(), cancellationToken)).Entity;

        string FormatDriverName(string name)
        {
            name = Regex.Replace(WebUtility.HtmlDecode(name).Trim(), @"\s+", " ");
            return (name.Length >= 3 && name[^3..].All(char.IsUpper))
                        ? name.Substring(0, name.Length - 3)
                        : name;
        }
    }
}
