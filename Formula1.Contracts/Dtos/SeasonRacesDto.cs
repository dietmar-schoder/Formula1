namespace Formula1.Contracts.Dtos;

public class SeasonRacesDto : SeasonDto
{
    public ICollection<RaceDto> Races { get; set; }
}
