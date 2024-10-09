namespace Formula1.Contracts.Dtos;

public class GrandPrixDto : GrandPrixBasicDto
{
    public ICollection<RaceBasicDto> Races { get; set; }
}
