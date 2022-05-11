using Application.Rephrase.DTOs;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Services.Rephrase;

namespace Application.Rephrase;

public interface IAppRephraseService
{
    public Task<RephraseResultDTO> Rephrase(RephraseRequestModel model, string uId);
}
