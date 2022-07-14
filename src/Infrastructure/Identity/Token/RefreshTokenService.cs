using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Synword.Application.Exceptions;
using Synword.Application.Interfaces.Identity.Token;
using Synword.Application.Utility.Token.DTOs;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Entities.Identity.Token;
using Synword.Persistence.Identity;

namespace Synword.Infrastructure.Identity.Token;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly AppIdentityDbContext _identityDb;
    private readonly IJwtService _jwtService;

    public RefreshTokenService(
        AppIdentityDbContext identityDb,
        IJwtService jwtService)
    {
        _identityDb = identityDb;
        _jwtService = jwtService;
    }
    
    public async Task<TokenDto> RefreshToken(
        string inputRefreshToken, 
        CancellationToken cancellationToken = default)
    {
        JwtSecurityToken encodedToken = _jwtService.EncodeJwtToken(inputRefreshToken);
        
        UserIdentity userIdentity = GetUserFromJwt(encodedToken);

        string deviceId = GetDeviceIdFromJwt(encodedToken);
        
        RefreshToken? currentToken = 
            userIdentity.RefreshTokens.FirstOrDefault(
                t => t.DeviceId == deviceId);

        if (currentToken is null)
        {
            throw new AppValidationException("Token does not exist");
        }

        await ValidateRefreshToken(inputRefreshToken, currentToken, cancellationToken);

        RefreshToken newToken = _jwtService.GenerateRefreshToken(
            userIdentity.Id,
            deviceId
        );
        
        _identityDb.RefreshTokens.Remove(currentToken);
        
        userIdentity.AddRefreshToken(newToken);
        
        _identityDb.Update(userIdentity);
        
        await _identityDb.SaveChangesAsync(cancellationToken);
        
        string accessToken = _jwtService.GenerateAccessToken(userIdentity.Id);

        return new TokenDto(accessToken, newToken.Token);
    }
    
    private async Task ValidateRefreshToken(
        string inputToken, 
        RefreshToken currentToken,
        CancellationToken cancellationToken = default)
    {
        _jwtService.ValidateJwtToken(inputToken);

        if (currentToken.Token != inputToken)
        {
            _identityDb.RefreshTokens.Remove(currentToken);
            await _identityDb.SaveChangesAsync(cancellationToken);

            throw new AppValidationException("Invalid token");
        }
    }
    
    private UserIdentity GetUserFromJwt(JwtSecurityToken encodedToken)
    {
        string uId = encodedToken.Claims.First(
            claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        UserIdentity? userIdentity = _identityDb.Users
            .Where(
                u => u.Id == uId)
            .Include(u => u.RefreshTokens)
            .FirstOrDefault();
        Guard.Against.Null(userIdentity);

        return userIdentity;
    }
    
    private string GetDeviceIdFromJwt(JwtSecurityToken encodedToken)
    {
        string? deviceId = encodedToken.Claims.First(
            claim => claim.Type == "deviceId").Value;
        Guard.Against.NullOrEmpty(deviceId);

        return deviceId;
    }
}
