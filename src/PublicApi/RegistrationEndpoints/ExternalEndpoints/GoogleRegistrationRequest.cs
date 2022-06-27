using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.RegistrationEndpoints.ExternalEndpoints;

public class GoogleRegistrationRequest
{
    [Required]
    public string? AccessToken { get; set; }
}
