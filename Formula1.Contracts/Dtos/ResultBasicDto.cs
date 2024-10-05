namespace Formula1.Contracts.Dtos;

public class ResultBasicDto
{
    public Guid Id { get; set; }

    public int Position { get; set; }

    public TimeSpan Time { get; set; }

    //public DriverDto Driver { get; set; }

    //public ConstructorDto Constructor { get; set; }
}
