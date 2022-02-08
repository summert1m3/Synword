using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Ip)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(u => u.Coins)
            .IsRequired();
        
        builder.Property(u => u.UsageData)
            .IsRequired();

        builder.OwnsOne(u => u.Coins, uc =>
        {
            uc.WithOwner();
            uc.Property(c => c.Value)
                .HasColumnName("Coins");
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
                .HasColumnName("Ip");
        });
        
        builder.OwnsOne(u => u.History, uh =>
        {
            uh.WithOwner();
        });
        
    }
}
