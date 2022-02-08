using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Infrastructure.Data;

namespace Synword.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<UserDataContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("UserDataConnection")));
        services.AddDbContext<DictionaryContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("DictionaryConnection")));
        services.AddDbContext<PricesContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("PricesConnection")));
    }
}
