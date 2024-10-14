namespace Formula1.Contracts.Dtos;

public class SessionBasicDto
{
    public Guid Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public SessionTypeDto SessionType { get; set; }

    public RaceBasicDto Race { get; set; }
}
