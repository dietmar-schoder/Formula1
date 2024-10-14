namespace Formula1.Contracts.Dtos;

public class RaceDto : RaceBasicDto
{
    public ICollection<SessionBasicDto> Sessions { get; set; }
}
