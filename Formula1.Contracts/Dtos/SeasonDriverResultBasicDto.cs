using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SeasonDriverResultBasicDto(
    int SessionId,
    string DriverName,
    int Position)
{
    public static SeasonDriverResultBasicDto FromResult(Result result)
        => new(
            SessionId: result.SessionId,
            DriverName: result.Driver.Name,
            Position: result.Position);
}
