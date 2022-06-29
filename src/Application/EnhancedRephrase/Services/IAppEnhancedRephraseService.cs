using Synword.Domain.Services.EnhancedRephrase;

namespace Application.EnhancedRephrase.Services;

public interface IAppEnhancedRephraseService
{
    public Task<EnhancedRephraseResult> Rephrase(
        EnhancedRephraseRequestModel request, string uId);
}
