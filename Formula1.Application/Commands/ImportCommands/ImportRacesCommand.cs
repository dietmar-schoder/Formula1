using MediatR;

namespace Formula1.Application.Commands.ImportCommands;

public class ImportRacesCommand(int year) : IRequest<Unit>
{
    public int Year { get; set; } = year;
}
