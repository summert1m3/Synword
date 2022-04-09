using Synword.Domain.Services.PlagiarismCheck;

namespace Synword.Domain.Interfaces;

public interface IPlagiarismCheckAPI
{
    public Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text);
}
