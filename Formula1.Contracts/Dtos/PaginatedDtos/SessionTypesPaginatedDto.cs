namespace Formula1.Contracts.Dtos.PaginatedDtos;

public record SessionTypesPaginatedDto(
    List<SessionTypeDto> SessionTypes,
    int PageNumber,
    int PageSize,
    int TotalCount);
