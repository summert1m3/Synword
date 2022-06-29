namespace Synword.Domain.Constants;

public interface IServiceConstraints
{
    public int PlagiarismCheckMaxSymbolLimit { get; }
    public int RephraseMaxSymbolLimit { get; }
    public int EnhancedRephraseMaxSymbolLimit { get; }
}
