using Synword.Application.AppFeatures.PlagiarismCheck.DTOs;

namespace Synword.Application.AppFeatures.PlagiarismCheck.Services;

public interface IAppPlagiarismCheckService
{
    public Task<PlagiarismCheckResultDto> CheckPlagiarism(
        string text, string uId);
}
