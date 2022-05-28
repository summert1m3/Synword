using System.Collections.Generic;
using System.Threading.Tasks;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Interfaces.Services;

namespace UnitTests.Domain.Services.PlagiarismCheckTests;

public class PlagiarismCheckApiStub : IPlagiarismCheckAPI
{
    public Task<PlagiarismCheckResult> CheckPlagiarism(string text)
    {
        return Task.Run(() => new PlagiarismCheckResult(
            text,
            50,
            new List<HighlightRange> {new(0, 5)},
            new List<MatchedUrl>
            {
                new(
                    "test.com",
                    50,
                    new List<HighlightRange> {new(5, 10)})
            }));
    }
}
