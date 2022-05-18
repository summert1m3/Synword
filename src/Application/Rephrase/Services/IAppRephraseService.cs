using Application.Rephrase.DTOs;

namespace Application.Rephrase;

public interface IAppRephraseService
{
    public Task<RephraseResultDTO> Rephrase(RephraseRequestModel model, string uId);
}
