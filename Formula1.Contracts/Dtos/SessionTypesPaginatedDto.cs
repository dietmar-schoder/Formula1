namespace Formula1.Contracts.Dtos;

public record SessionTypesPaginatedDto(
    List<SessionTypeDto> SessionTypes,
    int PageNumber,
    int PageSize,
    int TotalCount);
