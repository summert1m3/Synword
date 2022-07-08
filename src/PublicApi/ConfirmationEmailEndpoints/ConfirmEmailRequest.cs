using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.ConfirmationEmailEndpoints;

public class ConfirmEmailRequest
{
    [Required]
    public string ConfirmationCode { get; set; }
}
