using MinimalApi.Endpoint;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class UserAuthenticateEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/googleAuthenticate",
            async () =>
            {
                return await HandleAsync();
            });
    }

    public async Task<IResult> HandleAsync()
    {
        return Results.Ok();
    }
}
