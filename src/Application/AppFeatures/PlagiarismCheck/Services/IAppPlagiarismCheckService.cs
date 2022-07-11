using Application.AppFeatures.PlagiarismCheck.DTOs;

namespace Application.AppFeatures.PlagiarismCheck.Services;

public interface IAppPlagiarismCheckService
{
    public Task<PlagiarismCheckResultDto> CheckPlagiarism(
        string text, string uId);
}
