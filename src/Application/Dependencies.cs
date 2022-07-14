using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Application.AppFeatures.EnhancedRephrase.Services;
using Synword.Application.AppFeatures.PlagiarismCheck.Services;
using Synword.Application.AppFeatures.Rephrase.Services;
using Synword.Application.Guests.Services;
using Synword.Application.Users.Services;
using Synword.Application.Utility.Documents.Services;
using Synword.Application.Utility.Token.Services;
using Synword.Application.Validation.EnhancedRephraseValidation;
using Synword.Application.Validation.PlagiarismCheckValidation;
using Synword.Application.Validation.RephraseValidation;

namespace Synword.Application;

public static class Dependencies
{
    public static void AddApplication(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped(
            typeof(IGuestService), typeof(GuestService));
    
        services.AddScoped(
            typeof(IUserService), typeof(UserService));
        
        services.AddScoped(
            typeof(IAppPlagiarismCheckService), 
            typeof(AppPlagiarismCheckService));
        
        services.AddScoped(
            typeof(IAppRephraseService), 
            typeof(AppRephraseService));
        
        services.AddScoped(
            typeof(IAppDocxService), 
            typeof(AppDocxService));

        services.AddScoped(
            typeof(IRephraseRequestValidation), 
            typeof(RephraseRequestValidation));
        
        services.AddScoped(
            typeof(IPlagiarismRequestValidation), 
            typeof(PlagiarismRequestValidation));
        
        services.AddScoped(
            typeof(IAppEnhancedRephraseService), 
            typeof(AppEnhancedRephraseService));

        services.AddScoped(
            typeof(IEnhancedRephraseRequestValidation), 
            typeof(EnhancedRephraseRequestValidation));
        
        services.AddScoped(
            typeof(IAppTokenService), 
            typeof(AppTokenService));
    }
}
