namespace Formula1.Contracts.Dtos;

public class CircuitDto : CircuitBasicDto
{
    public ICollection<RaceBasicDto> Races { get; set; }
}
