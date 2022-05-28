using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.PlagiarismCheck;
using Xunit;

namespace UnitTests.Domain.Services;

public class PlagiarismCheckServiceTests
{
    private readonly Mock<IPlagiarismCheckAPI> _mockPlagiarismCheckApi = new();
    private readonly IPlagiarismCheckAPI _plagiarismCheckApiApiStub = 
        new PlagiarismCheckApiStub();

    [Fact]
    public async Task CheckPlagiarismUnderLimit_Text_PlagiarismCheckResult()
    {
        // Arrange
        string text = Text.GetText445Chars();
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
        string text = Text.GetText20470Chars();
        PlagiarismCheckResult expected = new PlagiarismCheckResult(
            text,
            50,
            new List<HighlightRange>
            {
                new(0, 5),
                new(3057, 3062)
            },
            new List<MatchedUrl>()
            {
                new(
                    "test.com",
                    50,
                    new List<HighlightRange> {new(5, 10)}),
                new(
                    "test.com",
                    50,
                    new List<HighlightRange> {new(3062, 3067)}),
            });

        PlagiarismCheckService plagiarismCheckService = new(
            _plagiarismCheckApiApiStub,
            null
        );

        // Act
        PlagiarismCheckResult actual =
            await plagiarismCheckService.CheckPlagiarism(text);

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }
}
