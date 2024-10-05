namespace Formula1.Domain.Entities;

public class Race
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public int Round { get; set; }

    public Guid CircuitId { get; set; }
    public Circuit Circuit { get; set; }
}
