using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record CircuitRaceDto(
    int Id,
    int SeasonYear,
    int Round,
    string WikipediaUrl,
    int GrandPrixId,
    string GrandPrixName)
{
    public static CircuitRaceDto FromRace(Race race)
        => new(
            race.Id,
            race.SeasonYear,
            race.Round,
            race.WikipediaUrl,
            race.GrandPrixId,
            race.GrandPrix.Name);
}
