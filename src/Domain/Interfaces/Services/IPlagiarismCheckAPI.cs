using Synword.Domain.Entities.PlagiarismCheckAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IPlagiarismCheckApi
{
    public Task<PlagiarismCheckResult> CheckPlagiarism(string text);
}
