using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class Metadata : BaseEntity
{
    private Metadata()
    {
        // required by EF
    }
    
    public Metadata(Email? email, string? country, string? name, string? picture, string? locale)
    {
        Email = email;
        Country = country;
        Name = name;
        Picture = picture;
        Locale = locale;
    }

    public Email? Email { get; private set; }
    public string? Country { get; private set; }
    public string? Name { get; private set; }
    public string? Picture { get; private set; }
    public string? Locale { get; private set; }
}
