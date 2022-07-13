using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Application.Interfaces;
using Synword.Application.Interfaces.Google;
using Synword.Application.Interfaces.Services.Documents;
using Synword.Application.Interfaces.Services.Email;
using Synword.Application.Interfaces.Services.Token;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.Docx;
using Synword.Infrastructure.Email.ConfirmEmailService;
using Synword.Infrastructure.Email.EmailService;
using Synword.Infrastructure.Google;
using Synword.Infrastructure.PlagiarismCheck;
using Synword.Infrastructure.Token;
using Synword.Infrastructure.YandexApi;

namespace Synword.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructure(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped(
            typeof(IGoogleApi), typeof(GoogleApi));
        
        services.AddScoped(
            typeof(IPlagiarismCheckApi), 
            typeof(PlagiarismCheckApi));
        
        services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();
        
        services.AddScoped(
            typeof(IDocxService), 
            typeof(DocxService));
        
        services.AddScoped(
            typeof(IYandexTranslateApi), 
            typeof(YandexTranslateApi));
        
        services.AddScoped(
            typeof(IEmailService),
            typeof(EmailService));
        
        services.AddScoped(
            typeof(IConfirmEmailService),
            typeof(ConfirmEmailService));
    }
}
