using Synword.Application.AppFeatures.EnhancedRephrase.DTOs;
using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.Application.AppFeatures.EnhancedRephrase.Services;

public interface IAppEnhancedRephraseService
{
    public Task<EnhancedRephraseResult> Rephrase(
        EnhancedRephraseRequestDto request, string uId);
}
