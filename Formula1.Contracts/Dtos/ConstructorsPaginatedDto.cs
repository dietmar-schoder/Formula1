namespace Formula1.Contracts.Dtos;

public record ConstructorsPaginatedDto(
    List<ConstructorDto> Constructors,
    int PageNumber,
    int PageSize,
    int TotalCount);
