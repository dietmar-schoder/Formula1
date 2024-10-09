using Formula1.Contracts.Requests;
using Formula1.Contracts.Responses;
using MediatR;

namespace Formula1.Application.Commands.ImportCommands;

public class ImportGrandPrixCommand(ImportRequest importRequest) : IRequest<ImportResponse>
{
    public int FromYear { get; set; } = importRequest.FromYear;
    public int ToYear { get; set; } = importRequest.ToYear;
}
