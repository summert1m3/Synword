using Application.AppFeatures.Rephrase.DTOs;
using Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using Synword.Domain.Interfaces.Services;

namespace Application.AppFeatures.Rephrase.Services;

public interface IAppRephraseService
{
    public Task<RephraseResultDto> Rephrase(
        RephraseRequestDto model, 
        ISynonymDictionaryService dictionaryService, 
        string uId);
}
