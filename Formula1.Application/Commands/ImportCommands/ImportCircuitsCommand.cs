using MediatR;

namespace Formula1.Application.Commands.ImportCommands
{
    public class ImportCircuitsCommand : IRequest<Unit>
    {
        public ImportCircuitsCommand() { }
    }
}
