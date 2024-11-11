namespace Formula1.Contracts.Dtos;

public record CircuitRacesDto(
    int Id,
    string Name,
    ICollection<RaceDto> Races)
    : CircuitDto(Id, Name);
