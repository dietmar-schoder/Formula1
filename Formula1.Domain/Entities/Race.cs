namespace Formula1.Domain.Entities;

public class Race
{
    public int Id { get; set; }

    public int SeasonYear { get; set; }
    public Season Season { get; set; }

    public int Round { get; set; }

    public string WikipediaUrl { get; set; }

    public int? CircuitId { get; set; }
    public virtual Circuit Circuit { get; set; }

    public int GrandPrixId { get; set; }
    public virtual GrandPrix GrandPrix { get; set; }

    public virtual ICollection<Session> Sessions { get; set; }
}
