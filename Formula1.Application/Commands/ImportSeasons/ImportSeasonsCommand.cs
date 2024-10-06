using MediatR;

namespace Formula1.Application.Commands.ImportSeasons
{
    public class ImportSeasonsCommand : IRequest<Unit>
    {
        public ImportSeasonsCommand() { }
    }
}
