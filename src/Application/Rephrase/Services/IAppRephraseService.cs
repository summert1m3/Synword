using Application.Rephrase.DTOs;

namespace Application.Rephrase.Services;

public interface IAppRephraseService
{
    public Task<RephraseResultDTO> Rephrase(RephraseRequestModel model, string uId);
}
