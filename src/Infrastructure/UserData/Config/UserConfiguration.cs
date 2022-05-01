using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Enums;

namespace Synword.Infrastructure.UserData.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.Roles)
            .IsRequired()
            .HasConversion(
                v => 
                    string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                v => 
                    v.Split(new[] { ',' })
                    .Select(e =>  Enum.Parse(typeof(Role), e))
                    .Cast<Role>()
                    .ToList()
            );
        
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
            ue.Property(e => e.Type)
                .HasConversion<string>();
            ue.HasIndex(e => e.Id).IsUnique();
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
