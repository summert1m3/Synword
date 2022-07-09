using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Exceptions;
using Application.Utility.Token.DTOs;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.Identity.Token;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.Identity;

namespace Application.Utility.Token.Services;

public class AppTokenService : IAppTokenService
{
    private readonly ITokenClaimsService _tokenService;
    private readonly AppIdentityDbContext _db;

    public AppTokenService(
        ITokenClaimsService tokenService,
        AppIdentityDbContext db)
    {
        _tokenService = tokenService;
        _db = db;
    }

    public async Task<TokenDto> RefreshToken(
        string inputToken,
        CancellationToken cancellationToken = default)
    {
        JwtSecurityToken encodedToken = 
            new JwtSecurityTokenHandler().ReadJwtToken(inputToken);

        string uId = encodedToken.Claims.First(
            claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        AppUser? user = _db.Users
            .Where(
                u => u.Id == uId)
            .Include(u => u.RefreshTokens)
            .FirstOrDefault();
        Guard.Against.Null(user);
        
        string deviceId = encodedToken.Claims.First(
            claim => claim.Type == "deviceId").Value;
        
        RefreshToken? currentToken = 
            user.RefreshTokens.FirstOrDefault(t => t.DeviceId == deviceId);

        if (currentToken is null)
        {
            throw new AppValidationException("Token does not exist");
        }

        await ValidateToken(inputToken, currentToken, cancellationToken);

        RefreshToken newToken = await _tokenService.GenerateRefreshToken(
            uId,
            deviceId
            );
        
        _db.RefreshTokens.Remove(user.RefreshTokens.First(
            t => t.Token == inputToken));
        
        user.AddRefreshToken(newToken);
        
        _db.Update(user);
        
        await _db.SaveChangesAsync(cancellationToken);
        
        string accessToken = await _tokenService!.GenerateAccessToken(user.Id);

        return new TokenDto(accessToken, newToken.Token);
    }

    private async Task ValidateToken(
        string inputToken, 
        RefreshToken currentToken,
        CancellationToken cancellationToken = default)
    {
        _tokenService.ValidateJwtToken(inputToken);

        if (currentToken.Token != inputToken)
        {
            _db.RefreshTokens.Remove(currentToken);
            await _db.SaveChangesAsync(cancellationToken);

            throw new AppValidationException("Invalid token");
        }
    }
}
