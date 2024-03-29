﻿using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.AuthEndpoints.AuthViaEmailEndpoints;

public class AuthViaEmailRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    public string Password { get; set; }
}
