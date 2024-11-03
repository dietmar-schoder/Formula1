namespace Formula1.Contracts.Dtos;

public record SessionResultsDto(
    Guid Id,
    DateTime StartDateTimeUtc,
    SessionTypeDto SessionType,
    RaceDto Race,
    ICollection<ResultDto> Results)
    : SessionDto(Id, StartDateTimeUtc, SessionType, Race);
