namespace Formula1.Contracts.Dtos;

public record SessionDto(
    int Id,
    DateTime StartDateTimeUtc,
    SessionTypeDto SessionType,
    RaceDto Race);
