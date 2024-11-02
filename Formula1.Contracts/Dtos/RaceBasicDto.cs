namespace Formula1.Contracts.Dtos;

public class RaceBasicDto
{
    public Guid Id { get; set; }

    public int SeasonYear { get; set; }

    public int Round { get; set; }

    public SeasonBasicDto Season { get; set; }

    public GrandPrixBasicDto GrandPrix { get; set; }

    public CircuitBasicDto Circuit { get; set; }
}
