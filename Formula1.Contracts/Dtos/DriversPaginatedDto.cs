namespace Formula1.Contracts.Dtos;

public record DriversPaginatedDto<T>(
    List<T> Drivers,
    int PageNumber,
    int PageSize,
    int TotalCount);
