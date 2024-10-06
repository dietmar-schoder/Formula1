using Formula1.Contracts.ImportErgastDtos;

namespace Formula1.Contracts.Services
{
    public interface IErgastApisClient
    {
        Task<List<Circuit>> GetCircuitsAsync();
        Task<List<Race>> GetRacesAsync(int year);
        Task<List<Season>> GetSeasonsAsync();
    }
}
