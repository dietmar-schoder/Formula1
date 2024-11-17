namespace Formula1.Contracts.Dtos;

public record SessionDto
{
    public int Id { get; set; }
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
