using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetDriverByIdQuery(Guid id) : IRequest<DriverDto>
{
    public Guid Id { get; set; } = id;
}
