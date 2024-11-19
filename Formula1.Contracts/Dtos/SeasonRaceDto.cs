using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SeasonRaceDto(
    int Id,
    int Round,
    int GrandPrixId,
    string GrandPrixName,
    int CircuitId,
    string CircuitName)
{
    public static SeasonRaceDto FromRace(Race race)
    => new(
        Id: race.Id,
        Round: race.Round,
        GrandPrixId: race.GrandPrixId,
        GrandPrixName: race.GrandPrix.Name,
        CircuitId: race.CircuitId ?? 0,
        CircuitName: race.Circuit.Name);
}
