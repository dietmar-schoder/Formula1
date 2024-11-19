namespace Formula1.Domain.Entities;

public class Result
{
    public int Id { get; set; }

    public int Position { get; set; }

    public string Ranking { get; set; }

    public int Points { get; set; }

    public TimeSpan Time { get; set; }

    public int SessionId { get; set; }
    public virtual Session Session { get; set; }

    public int DriverId { get; set; }
    public virtual Driver Driver { get; set; }

    public int ConstructorId { get; set; }
    public virtual Constructor Constructor { get; set; }
}
