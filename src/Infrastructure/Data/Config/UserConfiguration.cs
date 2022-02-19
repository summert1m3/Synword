using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

namespace Synword.Infrastructure.Data.Config;

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
