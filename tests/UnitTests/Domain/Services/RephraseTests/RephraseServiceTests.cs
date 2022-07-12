using System.Collections.Generic;
using System.Threading.Tasks;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.Rephrase;
using Xunit;

namespace UnitTests.Domain.Services.RephraseTests;

public class RephraseServiceTests
{
    private readonly IRephraseService _rephrase = new RephraseService();

    private readonly ISynonymDictionaryService _dictionary =
        new SynonymDictionaryServiceStub();

    [Fact]
    public void Rephrase_Text_RephraseResult()
    {
        // Arrange
        string text = Text.GetText445Chars();
        RephraseResult expected = new(
            text,
            Text.GetRephrasedText(),
            new List<SourceWordSynonyms>
            {
                new(
                    "Lorem",
                    0,
                    4,
                    new List<Synonym> {new("Velit")}
                ),
                new(
                    "dolor",
                    12,
                    16,
                    new List<Synonym> {new("lorem")}
                ),
                new(
                    "consectetur",
                    28,
                    31,
                    new List<Synonym> {new("sint")}
                ),
                new(
                    "aute",
                    230,
                    240,
                    new List<Synonym> {new("consectetur")}
                ),
                new(
                    "dolor",
                    248,
                    252,
                    new List<Synonym> {new("lorem")}
                ),
            }
        );

        // Act
        RephraseResult actual = _rephrase.Rephrase(text, _dictionary);

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }
}
