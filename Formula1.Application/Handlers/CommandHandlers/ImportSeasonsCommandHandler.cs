using Formula1.Application.Commands.ImportSeasons;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Services;
using MediatR;

namespace Formula1.Application.Handlers.CommandHandlers
{
    public class ImportSeasonsCommandHandler(IErgastApisClient ergastApisClient)
        : IRequestHandler<ImportSeasonsCommand, Unit>
    {
        private readonly IErgastApisClient _ergastApisClient = ergastApisClient;

        public async Task<Unit> Handle(ImportSeasonsCommand request, CancellationToken cancellationToken)
        {
            var ergastSeasonsData = await _ergastApisClient.GetSeasonsDataAsync();
            var seasonList = new List<SeasonDto>();
            foreach (var season in ergastSeasonsData.MRData.SeasonTable.Seasons)
            {
                seasonList.Add(new()
                {
                    Year = int.Parse(season.season),
                    WikipediaUrl = season.url
                });
            }

            return Unit.Value;
        }
    }
}
