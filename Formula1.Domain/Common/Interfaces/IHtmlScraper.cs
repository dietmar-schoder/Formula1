using HtmlAgilityPack;

namespace Formula1.Domain.Common.Interfaces;

public interface IHtmlScraper
{
    Task<HtmlNodeCollection> GetHtmlTable(HttpClient client, string url);
    string GetColumn(HtmlNode row, int columnNumber);
}
