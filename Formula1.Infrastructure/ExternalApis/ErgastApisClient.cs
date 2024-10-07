using Formula1.Contracts.ImportErgastDtos;
using Formula1.Contracts.Services;
using System.Net.Http.Json;

namespace Formula1.Infrastructure.ExternalApis;

public class ErgastApisClient(HttpClient client) : IErgastApisClient
{
    private readonly HttpClient _client = client;

    public async Task<List<Circuit>> GetCircuitsAsync()
    {
        var response = await _client.GetAsync("api/f1/circuits.json?limit=100");
        var data = await response.Content.ReadFromJsonAsync<ErgastImportData>();
        return data.MRData.CircuitTable.Circuits;
    }

    public async Task<List<Race>> GetRacesAsync(int year)
    {
        var response = await _client.GetAsync($"api/f1/{year}/races.json?limit=100");
        var data = await response.Content.ReadFromJsonAsync<ErgastImportData>();
        return data.MRData.RaceTable.Races;
    }

    public async Task<List<Season>> GetSeasonsAsync()
    {
        var response = await _client.GetAsync("api/f1/seasons.json?limit=100");
        var data = await response.Content.ReadFromJsonAsync<ErgastImportData>();
        return data.MRData.SeasonTable.Seasons;
    }
}
