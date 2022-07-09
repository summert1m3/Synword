using MinimalApi.Endpoint;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.UtilityEndpoints.MobileAppEndpoints.AppVersion;

public class MobileAppVersionEndpoint : IEndpoint<IResult>
{
    private IConfiguration _configuration;
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("clientAppVersion",
                [SwaggerOperation(
                    Tags = new[] { "Utility" }
                )]
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
