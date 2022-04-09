using Synword.Domain.Services.PlagiarismCheck;

namespace Synword.Domain.Interfaces;

public interface IPlagiarismCheckService
{
    public Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text);
}
