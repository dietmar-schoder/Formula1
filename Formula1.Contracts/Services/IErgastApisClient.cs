using Formula1.Contracts.Dtos;

namespace Formula1.Contracts.Services
{
    public interface IErgastApisClient
    {
        Task<ErgastSeasonsData> GetSeasonsDataAsync();
    }
}
