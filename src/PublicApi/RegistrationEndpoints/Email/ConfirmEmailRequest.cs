using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;

namespace Synword.PublicApi.RegistrationEndpoints.Email;

public class ConfirmEmailRequest
{
    [Required]
    public string ConfirmationCode { get; set; }
}
