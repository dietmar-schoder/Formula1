namespace Formula1.Contracts.Dtos;

public class DriverDto : DriverBasicDto
{
    public ICollection<ResultDto> Results { get; set; }
}
