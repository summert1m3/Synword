namespace Synword.Domain.Constants;

public class SilverUserServiceConstraints : IServiceConstraints
{
    public int PlagiarismCheckMaxSymbolLimit => 60000;
    public int RephraseMaxSymbolLimit => 60000;
}
