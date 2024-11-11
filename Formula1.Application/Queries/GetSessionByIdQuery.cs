using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetSessionByIdQuery(int id) : IRequest<SessionDto>
{
    public int Id { get; set; } = id;
}
