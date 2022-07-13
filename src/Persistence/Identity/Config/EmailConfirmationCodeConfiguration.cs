using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synword.Domain.Entities.Identity;

namespace Synword.Persistence.Identity.Config;

public class EmailConfirmationCodeConfiguration 
    : IEntityTypeConfiguration<EmailConfirmationCode>
{
    public void Configure(EntityTypeBuilder<EmailConfirmationCode> builder)
    {
        builder.OwnsOne(m => m.ConfirmationCode, me =>
        {
            me.WithOwner();
            me.Property(e => e.Code)
                .HasColumnName("Code");
        });
        
        builder.OwnsOne(m => m.Email, me =>
        {
            me.WithOwner();
            me.Property(e => e.Value)
                .HasColumnName("Email");
        });
    }
}
