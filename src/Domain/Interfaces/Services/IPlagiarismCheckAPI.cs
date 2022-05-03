using Synword.Domain.Entities.PlagiarismCheckAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IPlagiarismCheckAPI
{
    public Task<PlagiarismCheckResult> CheckPlagiarism(string text);
}
