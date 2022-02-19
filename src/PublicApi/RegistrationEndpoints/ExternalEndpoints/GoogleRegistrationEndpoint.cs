using System.Security.Claims;
using Ardalis.GuardClauses;
using MinimalApi.Endpoint;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Services.Google;

namespace Synword.PublicApi.RegistrationEndpoints.ExternalEndpoints;

public class GoogleRegistrationEndpoint : IEndpoint<IResult, GoogleRegistrationRequest>
{
    private IRepository<User>? _userRepository;
    private IGoogleApi? _googleApi;
    private HttpContext? _context;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/googleRegister",
            async (GoogleRegistrationRequest request, IRepository<User> userRepository,
                IGoogleApi googleApi, HttpContext context) =>
            {
                _userRepository = userRepository;
                _googleApi = googleApi;
                _context = context;
                return await HandleAsync(request);
            });
        app.MapPost("api/123",
            async ()=> " "
            );
    }

    public async Task<IResult> HandleAsync(GoogleRegistrationRequest request)
    {
        Guard.Against.Null(request.UserId, nameof(request.UserId));
        Guard.Against.Null(request.AccessToken, nameof(request.AccessToken));
        Guard.Against.Null(_userRepository, nameof(_userRepository));
        Guard.Against.Null(_googleApi, nameof(_googleApi));
        Guard.Against.Null(_context, nameof(_context));

        if (!_googleApi.IsAccessTokenValid(request.AccessToken))
        {
            throw new Exception("Invalid Access Token");
        }

        User? user = await _userRepository.GetByIdAsync(request.UserId);

        Guard.Against.Null(user, nameof(user));
        
        GoogleUserModel googleUserModel = _googleApi.GetGoogleUserData(request.AccessToken);

        if (user.Id != googleUserModel.Id)
        {
            throw new Exception("user.Id != googleUserModel.Id");
        }

        return Results.Ok();
    }
}
