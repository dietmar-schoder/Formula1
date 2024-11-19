using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SessionDto(
    int Id,
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
    public static SessionDto FromSession(Session session)
        => new(
            Id: session.Id,
            SessionTypeId: session.SessionTypeId,
            SessionTypeDescription: session.SessionType.Description,
            RaceId: session.RaceId,
            SeasonYear: session.Race.SeasonYear,
            Round: session.Race.Round,
            GrandPrixId: session.Race.GrandPrixId,
            GrandPrixName: session.Race.GrandPrix.Name,
            CircuitId: session.Race.CircuitId ?? 0,
            CircuitName: session.Race.Circuit.Name);
}