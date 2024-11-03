namespace Formula1.Contracts.Dtos;

public record GrandPrixPaginatedDto<T>(
    List<T> GrandPrix,
    int PageNumber,
    int PageSize,
    int TotalCount);
