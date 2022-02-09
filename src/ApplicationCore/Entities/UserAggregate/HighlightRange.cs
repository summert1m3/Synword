using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class HighlightRange : BaseEntity
{
    private HighlightRange()
    {
        // required by EF
    }
    
    public HighlightRange(int startIndex, int endIndex)
    {
        Guard.Against.Negative(startIndex, nameof(startIndex));
        Guard.Against.Negative(endIndex, nameof(endIndex));
        Guard.Against.OutOfRange(startIndex, nameof(startIndex), 0, int.MaxValue);
        Guard.Against.OutOfRange(endIndex, nameof(endIndex), 0, int.MaxValue);

        StartIndex = startIndex;
        EndIndex = endIndex;
    }
    
    public int StartIndex { get; private set; }
    public int EndIndex { get; private set; }
}
