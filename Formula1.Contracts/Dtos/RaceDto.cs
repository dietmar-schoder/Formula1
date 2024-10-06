namespace Formula1.Contracts.Dtos;

public class RaceDto : RaceBasicDto
{
    public SeasonBasicDto Season { get; set; }

    public CircuitBasicDto Circuit { get; set; }
}
