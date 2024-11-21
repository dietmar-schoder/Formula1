using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record GrandPrixDto(
    int Id,
    string Name,
    string WikipediaUrl)
{
    public static GrandPrixDto FromGrandPrix(GrandPrix grandPrix)
        => new(
            Id: grandPrix.Id,
            Name: grandPrix.Name,
            WikipediaUrl: grandPrix.WikipediaUrl);
}
