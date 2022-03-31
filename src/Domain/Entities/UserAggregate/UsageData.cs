using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.UserAggregate;

public class UsageData : BaseEntity
{
    private UsageData()
    {
        // required by EF
    }
    
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
    public DateTime LastVisitDate { get; private set; }
    public DateTime CreationDate { get; private set; }

    public void PlagiarismCheckCountIncrement()
    {
        PlagiarismCheckCount++;
    }

    public void RephraseCountIncrement()
    {
        RephraseCount++;
    }
}
