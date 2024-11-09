namespace Formula1.Domain.Entities;

public class Constructor
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string WikipediaUrl { get; set; }

    public ICollection<Result> Results { get; set; }
}
