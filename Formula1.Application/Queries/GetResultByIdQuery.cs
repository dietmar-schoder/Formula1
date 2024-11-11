using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetResultByIdQuery(int id) : IRequest<ResultDto>
{
    public int Id { get; set; } = id;
}
