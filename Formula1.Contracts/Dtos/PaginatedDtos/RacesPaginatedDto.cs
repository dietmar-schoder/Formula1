namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record RacesPaginatedDto(List<RaceDto> Races,
    int PageNumber,
    int PageSize,
    int TotalCount);
