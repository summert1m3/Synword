using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Entities.Identity.Token;
using Synword.Persistence.Identity.Config;

namespace Synword.Persistence.Identity;

public class AppIdentityDbContext : IdentityDbContext<UserIdentity>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EmailConfirmationCode> EmailConfirmationCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new EmailConfirmationCodeConfiguration());
    }
}
