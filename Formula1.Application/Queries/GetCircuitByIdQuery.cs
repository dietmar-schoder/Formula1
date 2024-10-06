using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetCircuitByIdQuery(Guid id) : IRequest<CircuitDto>
{
    public Guid Id { get; set; } = id;
}
