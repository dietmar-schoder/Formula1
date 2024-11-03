namespace Formula1.Contracts.Dtos;

public record ConstructorResultsDto(Guid Id, string Name, ICollection<ResultDto> Results)
    : ConstructorDto(Id, Name);
