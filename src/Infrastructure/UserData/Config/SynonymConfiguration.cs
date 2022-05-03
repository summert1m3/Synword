using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Infrastructure.UserData.Config;

public class SynonymConfiguration : IEntityTypeConfiguration<Synonym>
{
    public void Configure(EntityTypeBuilder<Synonym> builder)
    {
        var splitStringConverter = new ValueConverter<IReadOnlyCollection<string>, string>(
            v => string.Join(";", v), 
            v => v.Split(new[] { ';' }));
        var splitStringValueComparer = new ValueComparer<IReadOnlyCollection<string>>(
            (c1, c2) => new HashSet<string>(c1!).SetEquals(new HashSet<string>(c2!)),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );
        builder.Property(s => s.Synonyms).HasConversion(splitStringConverter, splitStringValueComparer);
    }
}
