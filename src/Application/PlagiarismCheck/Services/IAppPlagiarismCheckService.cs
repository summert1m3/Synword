using Application.PlagiarismCheck.DTOs;

namespace Application.PlagiarismCheck.Services;

public interface IAppPlagiarismCheckService
{
    public Task<PlagiarismCheckResponseDTO> CheckPlagiarism(string text);
}
