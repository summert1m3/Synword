using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Infrastructure.SynonymDictionary.RuSynonymDictionary.Config;

public class SynonymConfiguration : IEntityTypeConfiguration<Synonym>
{
    public void Configure(EntityTypeBuilder<Synonym> builder)
    {
        builder.Property(p => p.Value).HasColumnName("Synonym");
    }
}
