using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synword.Domain.Entities.Identity.Token;

namespace Infrastructure.Token;

public class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly IConfiguration _configuration;

    public IdentityTokenClaimService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(string uId)
    {
        var secretKey = _configuration["JWT_SECRET_KEY"];
        var claims = new List<Claim> {new(ClaimTypes.NameIdentifier, uId)};

        string token = Generate(
            secretKey,
            DateTime.UtcNow.AddMinutes(15),
            claims
        );

        return token;
    }

    public RefreshToken GenerateRefreshToken(
        string uId,
        string deviceId
    )
    {
        var secretKey = _configuration["JWT_SECRET_KEY"];
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, uId), 
            new("deviceId", deviceId)
        };

        DateTime expirationDate = DateTime.UtcNow.AddDays(30);

        string refreshToken = Generate(
            secretKey,
            expirationDate,
            claims
        );

        RefreshToken tokenModel = new(
            refreshToken,
            deviceId,
            expirationDate,
            DateTime.UtcNow
        );

        return tokenModel;
    }

    public void ValidateJwtToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["JWT_SECRET_KEY"])),
            ClockSkew = TimeSpan.Zero
        };

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        
        jwtSecurityTokenHandler.ValidateToken(token, validationParameters,
            out SecurityToken _);
    }

    private string Generate(
        string secretKey,
        DateTime expires,
        IEnumerable<Claim> claims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials =
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new(
            null,
            null,
            claims,
            DateTime.UtcNow,
            expires,
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
