using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetDriverByIdQuery(int id) : IRequest<DriverDto>
{
    public int Id { get; set; } = id;
}
