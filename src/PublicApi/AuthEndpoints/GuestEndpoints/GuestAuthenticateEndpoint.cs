using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateEndpoint : IEndpoint<IResult, GuestAuthenticateRequest>
{
    private UserManager<AppUser>? _userManager;
    private ITokenClaimsService? _tokenClaimsService;
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/guestAuthenticate",
            async (GuestAuthenticateRequest request, UserManager<AppUser> userManager, 
                ITokenClaimsService tokenClaimsService) =>
            {
                _userManager = userManager;
                _tokenClaimsService = tokenClaimsService;
                return await HandleAsync(request);
            }).Produces<GuestAuthenticateResponse>();
    }

    public async Task<IResult> HandleAsync(GuestAuthenticateRequest request)
    {
        Guard.Against.Null(request.UserId, nameof(request.UserId));
        
        Guid id;
        
        var isGuid = Guid.TryParse(request.UserId,out id);
        
        if (!isGuid)
        {
            throw new FormatException();
        }

        AppUser? guest = await _userManager!.FindByIdAsync(id.ToString());

        Guard.Against.Null(guest);
        
        string token = await _tokenClaimsService!.GetTokenAsync(guest.Id);
        
        return Results.Ok(new GuestAuthenticateResponse(token));
    }
}
