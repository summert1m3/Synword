namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

public class ExternalSignIn
{
    public ExternalSignIn(ExternalSignInType type, string externalKey)
    {
        ExternalSignInType = type;
        ExternalKey = externalKey;
    }
    public ExternalSignInType ExternalSignInType { get; }
    public string ExternalKey { get; }
}
