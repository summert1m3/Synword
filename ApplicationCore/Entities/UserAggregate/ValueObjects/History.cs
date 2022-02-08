namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

public class History
{
    public List<PlagiarismCheckHistory>? PlagiarismCheckHistory { get; }
    public List<RephraseHistory>? RephraseHistory { get; }
}
