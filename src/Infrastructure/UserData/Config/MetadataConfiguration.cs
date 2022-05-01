using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Infrastructure.UserData.Config;

public class MetadataConfiguration : IEntityTypeConfiguration<Metadata>
{
    public void Configure(EntityTypeBuilder<Metadata> builder)
    {
        builder.OwnsOne(m => m.Email, me =>
        {
            me.WithOwner();
            me.Property(i => i.Value)
                .HasColumnName("Email");
        });
    }
}
