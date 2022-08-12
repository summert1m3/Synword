using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.PlagiarismCheck;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Domain.Services.PlagiarismCheckTests;

public class PlagiarismCheckServiceTests
{
    private readonly Mock<IPlagiarismCheckApi> _mockPlagiarismCheckApi = new();
    private readonly IPlagiarismCheckApi _plagiarismCheckApiApiStub = 
        new PlagiarismCheckApiStub();

    [Fact]
    public async Task CheckPlagiarismUnderLimit_Text_PlagiarismCheckResult()
    {
        // Arrange
        string text = PreparedText.GetText445Chars();
        PlagiarismCheckResult expected = new PlagiarismCheckResult(
            text,
            50,
            new List<HighlightRange> {new(0, 5)},
            new List<MatchedUrl>()
            {
                new(
                    "test.com",
                    50,
                    new List<HighlightRange> {new(5, 10)})
            });

        _mockPlagiarismCheckApi.Setup(x =>
            x.CheckPlagiarism(text)).ReturnsAsync(
            new PlagiarismCheckResult(
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

        PlagiarismCheckService plagiarismCheckService = new(
            _mockPlagiarismCheckApi.Object,
            null);

        // Act
        PlagiarismCheckResult actual =
            await plagiarismCheckService.CheckPlagiarism(text);

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }

    [Fact]
    public async Task CheckPlagiarismOverLimit_Text_PlagiarismCheckResult()
    {
        // Arrange
        PlagiarismCheckResult expected = PreparedResults.GetPlagiarismCheckResult();

        PlagiarismCheckService plagiarismCheckService = new(
            _plagiarismCheckApiApiStub,
            null
        );

        // Act
        PlagiarismCheckResult actual =
            await plagiarismCheckService.CheckPlagiarism(
                PreparedText.GetText20470Chars());

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }
}
