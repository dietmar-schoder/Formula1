using Formula1.Contracts.Responses;

namespace Formula1.Application.Services;

public interface IVersionService
{
    Alive GetVersion();
}
