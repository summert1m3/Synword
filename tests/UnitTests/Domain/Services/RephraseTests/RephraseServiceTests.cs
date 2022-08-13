using System.Collections.Generic;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.Rephrase;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Domain.Services.RephraseTests;

public class RephraseServiceTests
{
    private readonly IRephraseService _rephrase = new RephraseService();

    private readonly ISynonymDictionaryService _dictionary =
        new SynonymDictionaryServiceStub();

    [Fact]
    public void Rephrase_ValidText_ValidResult()
    {
        // Arrange
        RephraseResult expected = PreparedResults.GetRephraseResult();

        // Act
        RephraseResult actual = _rephrase.Rephrase(PreparedText.GetText445Chars(),
            _dictionary);

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }
}
