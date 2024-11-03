namespace Formula1.Contracts.Dtos;

public class ResultDto
{
    public Guid Id { get; set; }

    public int Position { get; set; }

    public TimeSpan Time { get; set; }

    public SessionDto Session { get; set; }

    public DriverDto Driver { get; set; }

    public ConstructorDto Constructor { get; set; }
}
