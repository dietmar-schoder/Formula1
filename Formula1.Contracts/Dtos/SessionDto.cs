namespace Formula1.Contracts.Dtos;

public record SessionDto(
    Guid Id,
    DateTime StartDateTimeUtc,
    SessionTypeDto SessionType,
    RaceDto Race);
