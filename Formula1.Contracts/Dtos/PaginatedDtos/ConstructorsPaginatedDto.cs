namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record ConstructorsPaginatedDto<T>(
    List<T> Constructors,
    int PageNumber,
    int PageSize,
    int TotalCount);
