using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetRaceByIdQuery(Guid id) : IRequest<RaceSessionsDto>
{
    public Guid Id { get; set; } = id;
}
