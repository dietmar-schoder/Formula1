namespace Formula1.Domain.Entities;

public class GrandPrix
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string WikipediaUrl { get; set; }

    public ICollection<Race> Races { get; set; }
}
