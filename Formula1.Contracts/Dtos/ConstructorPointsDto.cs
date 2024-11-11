namespace Formula1.Contracts.Dtos;

public record ConstructorPointsDto(int Id, string Name, string WikipediaUrl, int Points)
    : ConstructorDto(Id, Name, WikipediaUrl)
{
    public int Points { get; set; } = Points;
}
