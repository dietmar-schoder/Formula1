using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Extensions
{
    public static class EndpointExtenstions
    {
        public static async Task<IResult> SendQueryAsync<TResponse>(
            this IMediator mediator,
            IRequest<TResponse> query,
            IScopedErrorService errorService,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            var result = await mediator.Send(query, cancellationToken);
            return result is null ? Results.NotFound(errorService.Errors) : Results.Ok(result);
        }
    }
}
