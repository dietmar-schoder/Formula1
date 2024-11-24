using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SeasonConstructorResultBasicDto(
    int SessionId,
    string ConstructorName,
    int Position)
{
    public static SeasonConstructorResultBasicDto FromResult(Result result)
        => new(
            SessionId: result.SessionId,
            ConstructorName: result.Constructor.Name,
            Position: result.Position);
}
