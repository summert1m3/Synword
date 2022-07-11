using Application.Rephrase.DTOs;
using Application.Rephrase.DTOs.RephraseResult;

namespace Application.Rephrase.Services;

public interface IAppRephraseService
{
    public Task<RephraseResultDto> Rephrase(RephraseRequestDto model, string uId);
}
