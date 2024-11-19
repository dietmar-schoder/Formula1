namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record CircuitsPaginatedDto(
    List<CircuitDto> Circuits,
    int PageNumber,
    int PageSize,
    int TotalCount);
