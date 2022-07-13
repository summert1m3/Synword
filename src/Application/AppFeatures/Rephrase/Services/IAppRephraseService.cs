using Synword.Application.AppFeatures.Rephrase.DTOs;
using Synword.Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using Synword.Domain.Interfaces.Services;

namespace Synword.Application.AppFeatures.Rephrase.Services;

public interface IAppRephraseService
{
    public Task<RephraseResultDto> Rephrase(
        RephraseRequestDto model, 
        ISynonymDictionaryService dictionaryService, 
        string uId);
}
