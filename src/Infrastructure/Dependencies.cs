using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Application.Interfaces;
using Synword.Application.Interfaces.Documents;
using Synword.Application.Interfaces.Email;
using Synword.Application.Interfaces.Google;
using Synword.Application.Interfaces.Identity.Token;
using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.Docx;
using Synword.Infrastructure.Email.ConfirmEmailService;
using Synword.Infrastructure.Email.EmailService;
using Synword.Infrastructure.Google;
using Synword.Infrastructure.Identity.Token;
using Synword.Infrastructure.Identity.UserIdentityServices;
using Synword.Infrastructure.PlagiarismCheck;
using Synword.Infrastructure.YandexApi;

namespace Synword.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructure(IServiceCollection services)
    {
        services.AddScoped(
            typeof(IGoogleApi), typeof(GoogleApi));
        
        services.AddScoped(
            typeof(IPlagiarismCheckApi), 
            typeof(PlagiarismCheckApi));
        
        services.AddScoped<IJwtService, JwtService>();
        
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
        
        services.AddScoped(
            typeof(IUserAuthService),
            typeof(UserAuthService));
        
        services.AddScoped(
            typeof(IUserRegistrationService),
            typeof(UserRegistrationService));
        
        services.AddScoped(
            typeof(IRefreshTokenService),
            typeof(RefreshTokenService));
    }
}
