using Application.AppFeatures.Rephrase.DTOs;
using Application.AppFeatures.Rephrase.DTOs.RephraseResult;

namespace Application.AppFeatures.Rephrase.Services;

public interface IAppRephraseService
{
    public Task<RephraseResultDto> Rephrase(RephraseRequestDto model, string uId);
}
