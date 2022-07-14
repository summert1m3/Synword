namespace Synword.Persistence.Entities.Identity.ValueObjects;

public class ExternalSignIn
{
    private ExternalSignIn()
    {
        // required by EF
    }
    
    public ExternalSignIn(ExternalSignInType type, string externalId)
    {
        Type = type;
        Id = externalId;
    }
    public ExternalSignInType Type { get; private set; }
    public string Id { get; private set; }
}
