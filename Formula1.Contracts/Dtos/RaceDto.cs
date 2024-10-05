namespace Formula1.Contracts.Dtos;

public class RaceDto
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public int Round { get; set; }

    public CircuitDto Circuit { get; set; }
}
