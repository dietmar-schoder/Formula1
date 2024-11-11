﻿namespace Formula1.Contracts.Dtos;

public record RaceSessionsDto(
    int Id,
    int SeasonYear,
    int Round,
    SeasonDto Season,
    GrandPrixDto GrandPrix,
    CircuitDto Circuit,
    ICollection<SessionDto> Sessions)
    : RaceDto(Id, SeasonYear, Round, Season, GrandPrix, Circuit);
