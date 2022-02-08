﻿using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class RephraseHistory : BaseEntity
{
    private RephraseHistory()
    {
        // required by EF
    }

    public RephraseHistory(string sourceText, string rephrasedText, List<Synonym> synonyms)
    {
        Guard.Against.NullOrEmpty(sourceText, nameof(sourceText));
        Guard.Against.NullOrEmpty(rephrasedText, nameof(rephrasedText));
        Guard.Against.Null(synonyms, nameof(synonyms));

        SourceText = sourceText;
        RephrasedText = rephrasedText;
    }
    
    public string SourceText { get; }
    public string? RephrasedText { get; init; }
    private readonly List<Synonym> _synonyms = new();
    public IReadOnlyCollection<Synonym> Synonyms => _synonyms.AsReadOnly();
}
