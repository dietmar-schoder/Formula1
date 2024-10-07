using Formula1.Domain.Common.Interfaces;
using HtmlAgilityPack;

namespace Formula1.Infrastructure.Services;

public class HtmlScraper : IHtmlScraper
{
    public async Task<HtmlNodeCollection> GetHtmlTable(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        var htmlContent = await response.Content.ReadAsStringAsync();
        var document = new HtmlDocument();
        document.LoadHtml(htmlContent);
        return document.DocumentNode.SelectNodes("//tbody/tr");
    }

    public string GetColumn(HtmlNode row, int columnNumber)
        => row.SelectSingleNode($".//td[{columnNumber}]/p/a").InnerText.Trim();
}
