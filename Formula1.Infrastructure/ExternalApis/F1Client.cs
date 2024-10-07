using Formula1.Contracts.F1Dtos;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using HtmlAgilityPack;

namespace Formula1.Infrastructure.ExternalApis;

public class F1Client(HttpClient client, IHtmlScraper htmlScraper) : IF1Client
{
    private readonly HttpClient _client = client;
    private readonly IHtmlScraper _htmlScraper = htmlScraper;

    public async Task<List<F1Constructor>> GetConstructorsAsync(int year)
    {
        return (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/team"))
            .Select(row => new F1Constructor { Name = _htmlScraper.GetColumn(row, 2) })
            .ToList();
    }
}
