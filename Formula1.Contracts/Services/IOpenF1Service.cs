using Formula1.Contracts.Dtos;

namespace Formula1.Contracts.Services
{
    public interface IOpenF1Service
    {
        Task<JobData_OLD> ListJobsAsync();
    }
}
