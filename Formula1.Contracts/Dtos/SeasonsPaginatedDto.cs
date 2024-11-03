namespace Formula1.Contracts.Dtos;

public record SeasonsPaginatedDto(
    List<SeasonDto> Seasons,
    int PageNumber,
    int PageSize,
    int TotalCount);
