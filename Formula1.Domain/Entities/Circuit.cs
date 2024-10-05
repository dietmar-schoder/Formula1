namespace Formula1.Domain.Entities;

public class Circuit
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Race> Races { get; set; }
}
