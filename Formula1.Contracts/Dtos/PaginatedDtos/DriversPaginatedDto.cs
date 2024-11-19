namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record DriversPaginatedDto<T>(
    List<T> Drivers,
    int PageNumber,
    int PageSize,
    int TotalCount);
