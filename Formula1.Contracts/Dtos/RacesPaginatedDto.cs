namespace Formula1.Contracts.Dtos;

public record RacesPaginatedDto<T>(
    List<T> Races,
    int PageNumber,
    int PageSize,
    int TotalCount);
