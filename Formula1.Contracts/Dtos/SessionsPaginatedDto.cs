namespace Formula1.Contracts.Dtos;

public record SessionsPaginatedDto(
    List<SessionDto> Sessions,
    int PageNumber,
    int PageSize,
    int TotalCount);
