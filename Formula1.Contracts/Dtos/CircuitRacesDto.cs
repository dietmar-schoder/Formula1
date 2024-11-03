namespace Formula1.Contracts.Dtos;

public record CircuitRacesDto(
    Guid Id,
    string Name,
    ICollection<RaceDto> Races)
    : CircuitDto(Id, Name);
