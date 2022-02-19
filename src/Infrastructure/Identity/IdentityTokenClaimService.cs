using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synword.ApplicationCore.Interfaces;

namespace Synword.Infrastructure.Identity;

public class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    
    public IdentityTokenClaimService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<string> GetTokenAsync(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT_SECRET_KEY"]);
        var user = await _userManager.FindByIdAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, id) };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
