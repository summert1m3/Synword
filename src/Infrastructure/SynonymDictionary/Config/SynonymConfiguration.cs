using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Infrastructure.SynonymDictionary.Config;

public class SynonymConfiguration : IEntityTypeConfiguration<DictionarySynonym>
{
    public void Configure(EntityTypeBuilder<DictionarySynonym> builder)
    {
        builder.Property(p => p.Value).HasColumnName("Synonym");
    }
}
