namespace Formula1.Domain.Entities;

public class Race
{
    public int Id { get; set; }

    public int Year { get; set; }

    public int Number { get; set; }

    public int CircuitId { get; set; }
    public Circuit Circuit { get; set; }
}
