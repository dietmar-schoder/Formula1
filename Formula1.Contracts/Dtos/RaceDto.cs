namespace Formula1.Contracts.Dtos;

public record RaceDto(
    int Id,
    int SeasonYear,
    int Round,
    SeasonDto Season,
    GrandPrixDto GrandPrix,
    CircuitDto Circuit);
