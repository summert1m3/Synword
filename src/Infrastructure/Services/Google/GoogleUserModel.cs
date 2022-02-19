using System.Text.Json.Serialization;

namespace Synword.Infrastructure.Services.Google;

public class GoogleUserModel
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    [JsonPropertyName("verified_email")]
    public string? VerifiedEmail { get; set; }
    public string? Name { get; set; }
    [JsonPropertyName("given_name")]
    public string? GivenName { get; set; }
    [JsonPropertyName("family_name")]
    public string? FamilyName { get; set; }
    public string? Picture { get; set; }
    public string? Locale { get; set; }
}
