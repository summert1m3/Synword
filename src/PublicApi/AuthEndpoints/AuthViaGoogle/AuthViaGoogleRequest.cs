using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.AuthEndpoints.AuthViaGoogle;

public class AuthViaGoogleRequest
{
    [Required]
    public string? AccessToken { get; set; }
}
