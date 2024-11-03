namespace Formula1.Contracts.Dtos;

public class ResultDto
{
    public Guid Id { get; set; }

    public int Position { get; set; }

    public TimeSpan Time { get; set; }

    public SessionBasicDto Session { get; set; }

    public DriverDto Driver { get; set; }

    public ConstructorBasicDto Constructor { get; set; }
}
