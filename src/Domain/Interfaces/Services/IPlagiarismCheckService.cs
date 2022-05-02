using Synword.Domain.Services.PlagiarismCheck;

namespace Synword.Domain.Interfaces.Services;

public interface IPlagiarismCheckService
{
    public Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text);
}
