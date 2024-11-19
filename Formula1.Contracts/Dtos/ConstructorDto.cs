using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record ConstructorDto(int Id, string Name, string WikipediaUrl)
{
    public static ConstructorDto FromConstructor(Constructor constructor)
        => new(
            Id: constructor.Id,
            Name: constructor.Name,
            WikipediaUrl: constructor.WikipediaUrl);
}
