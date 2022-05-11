using Application.PlagiarismCheck.DTOs;

namespace Application.PlagiarismCheck.Services;

public interface IAppPlagiarismCheckService
{
    public Task<PlagiarismCheckResultDTO> CheckPlagiarism(
        string text, string uId);
}
