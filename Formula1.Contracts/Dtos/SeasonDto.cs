namespace Formula1.Contracts.Dtos;

public class SeasonDto : SeasonBasicDto
{
    public ICollection<RaceBasicDto> Races { get; set; }
}
