namespace Synword.Domain.Constants;

public class DefaultUserServiceConstraints : IServiceConstraints
{
    public static int MinSymbolLimit => 100;
    public int PlagiarismCheckMaxSymbolLimit => 20000;
    public int RephraseMaxSymbolLimit => 20000;
}
