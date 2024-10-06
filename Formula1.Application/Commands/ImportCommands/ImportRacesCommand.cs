using MediatR;

namespace Formula1.Application.Commands.ImportCommands
{
    public class ImportRacesCommand : IRequest<Unit>
    {
        public ImportRacesCommand() { }
    }
}
