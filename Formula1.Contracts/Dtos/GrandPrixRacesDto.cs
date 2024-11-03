namespace Formula1.Contracts.Dtos;

public record GrandPrixRacesDto(
    Guid Id,
    string Name,
    ICollection<RaceDto> Races)
    : GrandPrixDto(Id, Name);
