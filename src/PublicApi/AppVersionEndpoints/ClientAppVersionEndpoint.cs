using MinimalApi.Endpoint;

namespace Synword.PublicApi.AppVersionEndpoints;

public class ClientAppVersionEndpoint : IEndpoint<IResult>
{
    private IConfiguration _configuration;
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("clientAppVersion", 
            async (IConfiguration configuration) =>
            {
                _configuration = configuration;
                
                return await HandleAsync();
            })
            .Produces<ClientAppVersionResponse>();
    }

    public async Task<IResult> HandleAsync()
    {
        return Results.Ok(new ClientAppVersionResponse(
            _configuration["ClientAppVersion"])
        );
    }
}
