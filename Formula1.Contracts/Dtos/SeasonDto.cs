namespace Formula1.Contracts.Dtos;

public class SeasonDto
{
    public int Year { get; set; }

    public string WikipediaUrl { get; set; }

    public ICollection<RaceDto> Races { get; set; }
}
