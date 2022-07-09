using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.UtilityEndpoints.ConfirmationEmailEndpoints;

public class ConfirmEmailRequest
{
    [Required]
    public string ConfirmationCode { get; set; }
}
