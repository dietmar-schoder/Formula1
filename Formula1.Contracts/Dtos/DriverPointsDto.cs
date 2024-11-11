namespace Formula1.Contracts.Dtos;

public record DriverPointsDto(int Id, string Name, string WikipediaUrl, int Points)
    : DriverDto(Id, Name, WikipediaUrl)
{
    public int Points { get; set; } = Points;
}
