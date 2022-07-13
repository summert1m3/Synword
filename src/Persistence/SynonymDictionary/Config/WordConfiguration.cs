using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Persistence.SynonymDictionary.Config;

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.Property(p => p.Value).HasColumnName("Word");
    }
}
