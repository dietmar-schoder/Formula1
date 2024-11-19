using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SessionResultDto(
    int Id,
    int Position,
    string Ranking,
    decimal Points,
    int ConstructorId,
    string ConstructorName,
    int DriverId,
    string DriverName)
{
    public static SessionResultDto FromResult(Result result)
    => new(
        Id: result.Id,
        Position: result.Position,
        Ranking: result.Ranking,
        Points: result.Points / 100m,
        ConstructorId: result.ConstructorId,
        ConstructorName: result.Constructor.Name,
        DriverId: result.DriverId,
        DriverName: result.Driver.Name);
}
