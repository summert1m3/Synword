namespace Synword.Domain.Constants;

public class GoldUserServiceConstraints : IServiceConstraints
{
    public int PlagiarismCheckMaxSymbolLimit => int.MaxValue;
    public int RephraseMaxSymbolLimit => int.MaxValue;
    public int EnhancedRephraseMaxSymbolLimit => 8000;
}
