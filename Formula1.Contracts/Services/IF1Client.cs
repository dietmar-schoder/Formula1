using Formula1.Contracts.F1Dtos;

namespace Formula1.Contracts.Services
{
    public interface IF1Client
    {
        //Task<List<Circuit>> GetCircuitsAsync();
        Task<List<F1Constructor>> GetConstructorsAsync(int year);
        //Task<List<Season>> GetSeasonsAsync();
    }
}
