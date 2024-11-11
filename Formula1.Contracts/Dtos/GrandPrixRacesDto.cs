namespace Formula1.Contracts.Dtos;

public record GrandPrixRacesDto(
    int Id,
    string Name,
    ICollection<RaceDto> Races)
    : GrandPrixDto(Id, Name);
