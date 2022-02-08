using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.Infrastructure.Data.Config;

public class SynonymConfiguration : IEntityTypeConfiguration<Synonym>
{
    public void Configure(EntityTypeBuilder<Synonym> builder)
    {
        var splitStringConverter = new ValueConverter<IEnumerable<string>, string>(
            v => string.Join(";", v), 
            v => v.Split(new[] { ';' }));
        builder.Property(s => s.Synonyms).HasConversion(splitStringConverter);
    }
}
