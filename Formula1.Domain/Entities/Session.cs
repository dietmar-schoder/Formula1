using Formula1.Domain.Enums;

namespace Formula1.Domain.Entities;

public class Session
{
    public Guid Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public SessionTypeEnum SessionTypeId { get; set; }

    public Guid RaceId { get; set; }
    public Race Race { get; set; }
}
