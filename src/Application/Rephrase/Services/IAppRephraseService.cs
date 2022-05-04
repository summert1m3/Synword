using Application.Rephrase.DTOs;

namespace Application.Rephrase;

public interface IAppRephraseService
{
    public Task<RephraseResponseDTO> Rephrase(string text);
}
