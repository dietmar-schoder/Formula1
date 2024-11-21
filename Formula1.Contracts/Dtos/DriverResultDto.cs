using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record DriverResultDto(
    int Id,
    int Position,
    string Ranking,
    decimal Points,
    int ConstructorId,
    string ConstructorName,
    int SessionId,
    int SessionTypeId,
    string SessionTypeDescription,
    int RaceId,
    int SeasonYear,
    int Round,
    int GrandPrixId,
    string GrandPrixName,
    int CircuitId,
    string CircuitName)
{
    public static DriverResultDto FromResult(Result result)
        => new(
            Id: result.Id,
            Position: result.Position,
            Ranking: result.Ranking,
            Points: result.Points / 100m,
            ConstructorId: result.ConstructorId,
            ConstructorName: result.Constructor.Name,
            SessionId: result.SessionId,
            SessionTypeId: result.Session.SessionTypeId,
            SessionTypeDescription: result.Session.SessionType.Description,
            RaceId: result.Session.RaceId,
            SeasonYear: result.Session.Race.SeasonYear,
            Round: result.Session.Race.Round,
            GrandPrixId: result.Session.Race.GrandPrixId,
            GrandPrixName: result.Session.Race.GrandPrix.Name,
            CircuitId: result.Session.Race.CircuitId ?? 0,
            CircuitName: result.Session.Race.Circuit?.Name);
}
