namespace Formula1.Contracts.Dtos;

public record DriverResultsDto(Guid Id, string Name, ICollection<ResultDto> Results)
    : DriverDto(Id, Name);
