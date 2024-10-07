using Formula1.Contracts.F1Dtos;

namespace Formula1.Contracts.Services
{
    public interface IF1Client
    {
        Task<List<F1Constructor>> GetConstructorsAsync(int year);

        Task<List<F1Driver>> GetDriversAsync(int year);
    }
}
