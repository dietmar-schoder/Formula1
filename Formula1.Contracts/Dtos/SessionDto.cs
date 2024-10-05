namespace Formula1.Contracts.Dtos;

public class SessionDto
{
    public Guid Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public SessionTypeDto SessionType { get; set; }

    public RaceDto Race { get; set; }

    public ICollection<ResultBasicDto> Results { get; set; }
}
