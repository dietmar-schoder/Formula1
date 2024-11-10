namespace Formula1.Contracts.Dtos;

public record ConstructorResultDto
{
    public Guid Id { get; set; }
    public int Position { get; set; }
    public string Ranking { get; set; }
    public int Points { get; set; }
    public Guid ConstructorId { get; set; }
    public string ConstructorName { get; set; }
    public Guid DriverId { get; set; }
    public string DriverName { get; set; }
    public Guid SessionId { get; set; }
    public int SessionTypeId { get; set; }
    public string SessionTypeDescription { get; set; }
    public Guid RaceId { get; set; }
    public int SeasonYear { get; set; }
    public int Round { get; set; }
    public Guid GrandPrixId { get; set; }
    public string GrandPrixName { get; set; }
    public Guid CircuitId { get; set; }
    public string CircuitName { get; set; }
}
