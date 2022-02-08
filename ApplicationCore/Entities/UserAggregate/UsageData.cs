using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class UsageData : BaseEntity
{
    public UsageData(int plagiarismCheckCount, int rephraseCount, DateTime lastVisitDate, DateTime creationDate)
    {
        Guard.Against.
            OutOfRange(plagiarismCheckCount, nameof(plagiarismCheckCount), 0, int.MaxValue);
        Guard.Against.
            OutOfRange(rephraseCount, nameof(rephraseCount), 0, int.MaxValue);
        Guard.Against.Null(lastVisitDate, nameof(lastVisitDate));
        Guard.Against.Null(creationDate, nameof(creationDate));
        
        PlagiarismCheckCount = plagiarismCheckCount;
        RephraseCount = rephraseCount;
        LastVisitDate = lastVisitDate;
        CreationDate = creationDate;
    }

    public int PlagiarismCheckCount { get; private set; }
    public int RephraseCount { get; private set; }
    public DateTime LastVisitDate { get; }
    public DateTime CreationDate { get; }

    public void PlagiarismCheckCountIncrement()
    {
        PlagiarismCheckCount++;
    }

    public void RephraseCountIncrement()
    {
        RephraseCount++;
    }
}
