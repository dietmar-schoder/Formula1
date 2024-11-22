using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record RaceDto(
    int Id,
    int SeasonYear,
    string SeasonWikipediaUrl,
    int Round,
    string WikipediaUrl,
    int GrandPrixId,
    string GrandPrixName,
    string GrandPrixWikipediaUrl,
    int CircuitId,
    string CircuitName,
    string CircuitWikipediaUrl)
{
    public static RaceDto FromRace(Race race)
        => new(
            race.Id,
            race.SeasonYear,
            race.Season.WikipediaUrl,
            race.Round,
            race.WikipediaUrl,
            race.GrandPrixId,
            race.GrandPrix.Name,
            race.GrandPrix.WikipediaUrl,
            race.CircuitId ?? 0,
            race.Circuit.Name,
            race.Circuit.WikipediaUrl);
}
