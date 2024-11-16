namespace Formula1.Contracts.Dtos;

public record ConstructorResultDto
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Ranking { get; set; }
    public decimal Points { get; set; }
    public int ConstructorId { get; set; }
    public string ConstructorName { get; set; }
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int SessionId { get; set; }
    public int SessionTypeId { get; set; }
    public string SessionTypeDescription { get; set; }
    public int RaceId { get; set; }
    public int SeasonYear { get; set; }
    public int Round { get; set; }
    public int GrandPrixId { get; set; }
    public string GrandPrixName { get; set; }
    public int CircuitId { get; set; }
    public string CircuitName { get; set; }
}
