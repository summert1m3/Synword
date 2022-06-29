using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.Domain.Interfaces.Services;

public interface IEnhancedRephraseService
{
    public Task<EnhancedRephraseResult> Rephrase(string text);
}
