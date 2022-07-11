using Application.PlagiarismCheck.DTOs;

namespace Application.PlagiarismCheck.Services;

public interface IAppPlagiarismCheckService
{
    public Task<PlagiarismCheckResultDto> CheckPlagiarism(
        string text, string uId);
}
