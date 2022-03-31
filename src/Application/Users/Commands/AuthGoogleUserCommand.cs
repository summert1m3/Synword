using Application.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Application.Users.Commands;

public class AuthGoogleUserCommand : IRequest<UserAuthenticateDTO>
{
    public AuthGoogleUserCommand(string accessToken)
    {
        AccessToken = accessToken;
    }
    
    public string? AccessToken { get; }
}

internal class AuthGoogleUserCommandHandler :
    IRequestHandler<AuthGoogleUserCommand, UserAuthenticateDTO>
{
    private readonly IRepository<User>? _userRepository;
    private readonly IGoogleApi? _googleApi;
    private readonly ITokenClaimsService? _tokenClaimsService;

    public AuthGoogleUserCommandHandler(IGoogleApi googleApi, 
        IRepository<User> userRepository, ITokenClaimsService tokenClaimsService,
        UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<UserAuthenticateDTO> Handle(
        AuthGoogleUserCommand request, CancellationToken cancellationToken)
    {
        GoogleUserModel googleUserModel = await _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? user = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);

        string token = await _tokenClaimsService!.GetTokenAsync(user.Id);

        return new UserAuthenticateDTO {Token = token};
    }
}
