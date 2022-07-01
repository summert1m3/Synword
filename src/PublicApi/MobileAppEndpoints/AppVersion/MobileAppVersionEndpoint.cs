using MinimalApi.Endpoint;

namespace Synword.PublicApi.MobileAppEndpoints.AppVersion;

public class MobileAppVersionEndpoint : IEndpoint<IResult>
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
            .Produces<MobileAppVersionResponse>();
    }

    public async Task<IResult> HandleAsync()
    {
        return Results.Ok(new MobileAppVersionResponse(
            _configuration["ClientAppVersion"])
        );
    }
}
