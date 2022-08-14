using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synword.Persistence.Identity;
using Synword.Persistence.SynonymDictionary.EngSynonymDictionary;
using Synword.Persistence.SynonymDictionary.RusSynonymDictionary;
using Synword.Persistence.Synword;

namespace IntegrationTests.PublicApi;

[TestClass]
public class TestBase
{
    private static WebApplicationFactory<Program> _application;

    public static HttpClient NewClient
    {
        get
        {
            return _application.CreateClient();
        }
    }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _)
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(ConfigureServices);
                });
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IStartupFilter, StartupFilter>();
    }
}
