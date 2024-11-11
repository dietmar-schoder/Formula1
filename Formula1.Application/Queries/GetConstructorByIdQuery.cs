using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetConstructorByIdQuery(int id) : IRequest<ConstructorDto>
{
    public int Id { get; set; } = id;
}
