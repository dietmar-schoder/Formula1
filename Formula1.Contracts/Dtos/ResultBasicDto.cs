namespace Formula1.Contracts.Dtos;

public record ResultBasicDto
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
}
