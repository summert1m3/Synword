using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Infrastructure.Data;
using Synword.Infrastructure.Identity;

namespace Synword.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<UserDataContext>(c =>
            c.UseSqlServer(configuration["UserDataConnection"]));
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(configuration["IdentityConnection"]));
    }
}
