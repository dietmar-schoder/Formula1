namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record ResultsPaginatedDto<T>(
    List<T> Results,
    int PageNumber,
    int PageSize,
    int TotalCount)
{
    public ResultsPaginatedDto(List<T> results)
        : this(results, 1, results.Count, results.Count) { }
}
