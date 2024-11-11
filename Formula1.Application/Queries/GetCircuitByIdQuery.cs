using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetCircuitByIdQuery(int id) : IRequest<CircuitRacesDto>
{
    public int Id { get; set; } = id;
}
