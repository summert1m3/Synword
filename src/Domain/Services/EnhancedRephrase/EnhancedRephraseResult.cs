namespace Synword.Domain.Services.EnhancedRephrase;

public class EnhancedRephraseResult
{
    public EnhancedRephraseResult(string rephrasedText)
    {
        RephrasedText = rephrasedText;
    }
    
    public string RephrasedText { get; private set; }
}
