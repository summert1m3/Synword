using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateRequest
{
    [Required]
    public string? UserId { get; set; }
}
