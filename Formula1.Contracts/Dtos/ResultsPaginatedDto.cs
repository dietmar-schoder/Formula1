namespace Formula1.Contracts.Dtos;

public record ResultsPaginatedDto<T>(
    List<T> Results,
    int PageNumber,
    int PageSize,
    int TotalCount);
