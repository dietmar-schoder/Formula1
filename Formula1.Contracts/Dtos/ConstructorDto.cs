namespace Formula1.Contracts.Dtos;

public class ConstructorDto : ConstructorBasicDto
{
    public ICollection<ResultDto> Results { get; set; }
}
