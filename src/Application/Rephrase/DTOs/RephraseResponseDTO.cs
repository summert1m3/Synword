using Synword.Domain.Entities.RephraseAggregate;

namespace Application.Rephrase.DTOs;

public class RephraseResponseDTO
{
    public string SourceText { get; }
    public string? RephrasedText { get; }
    private readonly List<Synonym> _synonyms;
    public IReadOnlyCollection<Synonym> Synonyms => _synonyms.AsReadOnly();
}
