using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateRequest
{
    [Required]
    public string? AccessToken { get; set; }
}
