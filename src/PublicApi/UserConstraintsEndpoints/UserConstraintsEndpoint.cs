using MinimalApi.Endpoint;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Domain.Constants;

namespace Synword.PublicApi.UserConstraintsEndpoints;

public class UserConstraintsEndpoint : IEndpoint<IResult>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("userContraints", 
            [SwaggerOperation(
                Tags = new[] { "Utility" }
            )]
            async () => await HandleAsync()
        ).Produces<UserConstraintsResponse>();
    }
    
    public async Task<IResult> HandleAsync()
    {
        UserConstraintsResponse response = new()
        {
            Default = new DefaultUserServiceConstraints(),
            Silver = new SilverUserServiceConstraints(),
            Gold = new GoldUserServiceConstraints()
        };

        return Results.Ok(response);
    }
}
