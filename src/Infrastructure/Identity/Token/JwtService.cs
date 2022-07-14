using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synword.Persistence.Entities.Identity.Token;
using Synword.Persistence.Identity;

namespace Synword.Infrastructure.Identity.Token;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly AppIdentityDbContext _identityDb;

    public JwtService(
        IConfiguration configuration,
        AppIdentityDbContext identityDb)
    {
        _configuration = configuration;
        _identityDb = identityDb;
    }

    public string GenerateAccessToken(string uId)
    {
        var secretKey = _configuration["JWT_SECRET_KEY"];
        var claims = new List<Claim> {new(ClaimTypes.NameIdentifier, uId)};

        string token = GenerateJwt(
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

        string refreshToken = GenerateJwt(
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

    private string GenerateJwt(
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
    
    public JwtSecurityToken EncodeJwtToken(string jwt)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(jwt);
    }
    





}
