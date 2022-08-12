using System.Collections.Generic;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;

namespace UnitTests.Utils;

public static class PreparedResults
{
    public static RephraseResult GetRephraseResult()
    {
        return new RephraseResult(
            PreparedText.GetText445Chars(),
            PreparedText.GetRephrasedText(),
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
    }

    public static PlagiarismCheckResult GetPlagiarismCheckResult()
    {
        return new PlagiarismCheckResult(
            PreparedText.GetText20470Chars(),
            50,
            new List<HighlightRange> {new(0, 5), new(3057, 3062)},
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
    }
}
