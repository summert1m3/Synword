namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

public class ExternalSignIn
{
    private ExternalSignIn()
    {
        // required by EF
    }
    
    public ExternalSignIn(ExternalSignInType type, string externalKey)
    {
        ExternalSignInType = type;
        ExternalKey = externalKey;
    }
    public ExternalSignInType ExternalSignInType { get; private set; }
    public string ExternalKey { get; private set; }
}
