namespace Formula1.Domain.Entities;

public class Session
{
    public int Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public int SessionTypeId { get; set; }
    public virtual SessionType SessionType { get; set; }

    public int RaceId { get; set; }
    public virtual Race Race { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}
