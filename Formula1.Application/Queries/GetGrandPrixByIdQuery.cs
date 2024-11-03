using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetGrandPrixByIdQuery(Guid id) : IRequest<GrandPrixRacesDto>
{
    public Guid Id { get; set; } = id;
}
