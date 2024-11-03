namespace Formula1.Contracts.Dtos;

public record DriversPaginatedDto(
    List<DriverDto> Drivers,
    int PageNumber,
    int PageSize,
    int TotalCount);
