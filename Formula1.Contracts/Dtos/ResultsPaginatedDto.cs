namespace Formula1.Contracts.Dtos;

public record ResultsPaginatedDto(
    List<ResultDto> Results,
    int PageNumber,
    int PageSize,
    int TotalCount);
