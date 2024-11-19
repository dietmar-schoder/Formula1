using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record SeasonDto(int Year, string WikipediaUrl)
{
    public static SeasonDto FromSeason(Season season)
        => new(
            Year: season.Year,
            WikipediaUrl: season.WikipediaUrl);
}
