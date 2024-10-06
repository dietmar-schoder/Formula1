namespace Formula1.Contracts.Dtos;

public class RaceDto
{
    public Guid Id { get; set; }

    public int SeasonYear { get; set; }

    public int Round { get; set; }

    public CircuitDto Circuit { get; set; }
}
