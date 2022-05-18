using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.RephraseAggregate;

namespace Synword.Infrastructure.Synword.Config;

public class SynonymConfiguration : IEntityTypeConfiguration<Synonym>
{
    public void Configure(EntityTypeBuilder<Synonym> builder)
    {
        builder.Property(u => u.Value).HasColumnName("Synonym");
    }
}
