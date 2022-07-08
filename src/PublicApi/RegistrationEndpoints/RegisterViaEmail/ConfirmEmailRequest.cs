using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.RegistrationEndpoints.RegisterViaEmail;

public class ConfirmEmailRequest
{
    [Required]
    public string ConfirmationCode { get; set; }
}
