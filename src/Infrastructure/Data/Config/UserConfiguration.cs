using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();
        
        builder.OwnsOne(u => u.Coins, uc =>
        {
            uc.WithOwner();
            uc.Property(c => c.Value)
                .HasColumnName("Coins")
                .IsRequired();
        });

        builder.OwnsOne(u => u.ExternalSignIn, ue =>
        {
            ue.WithOwner();
            ue.Property(e => e.ExternalSignInType)
                .HasConversion<int>();
        });
        
        builder.OwnsOne(u => u.Ip, ui =>
        {
            ui.WithOwner();
            ui.Property(i => i.Value)
                .HasColumnName("Ip")
                .IsRequired();
        });
    }
}
