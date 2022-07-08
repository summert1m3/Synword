using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.RegistrationEndpoints.RegisterViaGoogle;

public class RegisterViaGoogleRequest
{
    [Required]
    public string? AccessToken { get; set; }
}
