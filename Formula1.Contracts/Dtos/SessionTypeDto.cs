using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SessionTypeDto(
    int Id,
    string Description)
{
    public static SessionTypeDto FromSessionType(SessionType sessionType)
        => new(
            sessionType.Id,
            sessionType.Description);
}
