namespace Formula1.Contracts.Dtos;

public record CircuitsPaginatedDto(
    List<CircuitDto> Circuits,
    int PageNumber,
    int PageSize,
    int TotalCount);
