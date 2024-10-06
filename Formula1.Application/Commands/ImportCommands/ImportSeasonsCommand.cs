using MediatR;

namespace Formula1.Application.Commands.ImportCommands
{
    public class ImportSeasonsCommand : IRequest<Unit>
    {
        public ImportSeasonsCommand() { }
    }
}
