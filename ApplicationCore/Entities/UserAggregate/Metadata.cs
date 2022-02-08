using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class Metadata : BaseEntity
{
    public Metadata(Email? email, string? country, string? name, string? picture, string? locale)
    {
        Email = email;
        Country = country;
        Name = name;
        Picture = picture;
        Locale = locale;
    }

    public Email? Email { get; }
    public string? Country { get; }
    public string? Name { get; }
    public string? Picture { get; }
    public string? Locale { get; }
}
