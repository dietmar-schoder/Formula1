using Formula1.Contracts.Requests;
using MediatR;

namespace Formula1.Application.Commands.ImportCommands;

public class ImportConstructorsCommand(ImportRequest importRequest) : IRequest<Unit>
{
    public int FromYear { get; set; } = importRequest.FromYear;
    public int ToYear { get; set; } = importRequest.ToYear;
}
