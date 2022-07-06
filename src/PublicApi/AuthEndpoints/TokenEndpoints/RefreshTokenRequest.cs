using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.AuthEndpoints.TokenEndpoints;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}
