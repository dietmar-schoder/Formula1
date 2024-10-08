namespace Formula1.Contracts.Dtos;

public class ConstructorDto : ConstructorBasicDto
{
    public ICollection<ResultBasicDto> Results { get; set; }
}
