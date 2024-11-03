namespace Formula1.Contracts.Dtos;

public record GrandPrixPaginatedDto(
    List<GrandPrixDto> GrandPrix,
    int PageNumber,
    int PageSize,
    int TotalCount);
