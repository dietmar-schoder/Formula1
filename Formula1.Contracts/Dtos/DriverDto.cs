using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record DriverDto(
    int Id,
    string Name,
    string WikipediaUrl)
{
    public static DriverDto FromDriver(Driver driver)
        => new(
            Id: driver.Id,
            Name: driver.Name,
            WikipediaUrl: driver.WikipediaUrl);
}
