using Application.EnhancedRephrase.DTOs;
using Synword.Domain.Services.EnhancedRephrase;

namespace Application.EnhancedRephrase.Services;

public interface IAppEnhancedRephraseService
{
    public Task<EnhancedRephraseResult> Rephrase(
        EnhancedRephraseRequestDto request, string uId);
}
