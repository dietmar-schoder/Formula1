using Formula1.Contracts.Dtos;
using Formula1.Contracts.Services;
using System.Net.Http.Json;

namespace Formula1.Infrastructure.ExternalApis;

public class ErgastApisClient(HttpClient client) : IErgastApisClient
{
    private readonly HttpClient _client = client;

    public string BaseAddress => _client.BaseAddress?.AbsoluteUri;

    public static string GetSeasonDataUrl()
        => $"api/f1/seasons.json?limit=100"; //https://ergast.com/api/f1/seasons.json?limit=100

    public async Task<ErgastSeasonsData> GetSeasonsDataAsync()
    {
        var response = await _client.GetAsync(GetSeasonDataUrl());
        var contentType = response.Content.Headers.ContentType;
        //if (contentType != null && contentType.CharSet == "utf8")
        //{
        //    contentType.CharSet = "utf-8";
        //}
        var ergastSeason = await response.Content.ReadFromJsonAsync<ErgastSeasonsData>();
        return ergastSeason;
    }
}
