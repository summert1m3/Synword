using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Persistence.Entities.Identity;

namespace Synword.Persistence.Identity.Config;

public class UserIdentityConfig : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.OwnsOne(u => u.ExternalSignIn, ue =>
        {
            ue.WithOwner();
            ue.Property(e => e.Type)
                .HasConversion<string>();
            ue.HasIndex(e => e.Id).IsUnique();
        });
    }
}
