using Synword.Domain.Entities.PlagiarismCheckAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IPlagiarismCheckService
{
    public Task<PlagiarismCheckResult> CheckPlagiarism(string text);
}
