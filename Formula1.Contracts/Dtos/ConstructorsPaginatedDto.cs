namespace Formula1.Contracts.Dtos;

public record ConstructorsPaginatedDto<T>(
    List<T> Constructors,
    int PageNumber,
    int PageSize,
    int TotalCount);
