using Formula1.Contracts.F1Dtos;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;

namespace Formula1.Infrastructure.ExternalApis;

public class F1Client(HttpClient client, IHtmlScraper htmlScraper) : IF1Client
{
    private readonly HttpClient _client = client;
    private readonly IHtmlScraper _htmlScraper = htmlScraper;

    public async Task<List<F1Constructor>> GetConstructorsAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/team"))
            .Select(row => new F1Constructor { Name = _htmlScraper.GetColumn(row, 2) })
            .ToList();

    public async Task<List<F1Driver>> GetDriversAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/drivers"))
            .Select(row => new F1Driver { Name = _htmlScraper.GetColumn(row, 2) })
            .ToList();

    public async Task<List<F1GrandPrix>> GetGrandPrixAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/races"))
            .Select(row => new F1GrandPrix { Name = _htmlScraper.GetColumn(row, 1) })
            .ToList();
}
