using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.UtilityEndpoints.RefreshTokenEndpoints;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}
