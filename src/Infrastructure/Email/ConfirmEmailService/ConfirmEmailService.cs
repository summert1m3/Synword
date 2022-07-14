using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Application.Exceptions;
using Synword.Application.Interfaces.Email;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Entities.Identity.ValueObjects;
using Synword.Persistence.Identity;

namespace Synword.Infrastructure.Email.ConfirmEmailService;

public class ConfirmEmailService : IConfirmEmailService
{
    private readonly AppIdentityDbContext _db;
    private readonly UserManager<UserIdentity> _userManager;

    public ConfirmEmailService(
        AppIdentityDbContext db,
        UserManager<UserIdentity> userManager)
    {
        _db = db;
        _userManager = userManager;
    }
    
    public async Task<string> GenerateNewConfirmCode(string uId)
    {
        UserIdentity userIdentity = await _userManager.FindByIdAsync(uId);
        Guard.Against.Null(userIdentity);

        string email = userIdentity.Email;
        Guard.Against.NullOrEmpty(email);
        
        RemoveOldCodeIfExist(email);

        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        string code = _rdm.Next(_min, _max).ToString();

        EmailConfirmationCode codeModel = new EmailConfirmationCode(
            new ConfirmationCode(code),
            new Synword.Domain.Entities.UserAggregate.ValueObjects.Email(email));

        await _db.AddAsync(codeModel);
        await _db.SaveChangesAsync();

        return code;
    }

    public async Task ConfirmEmail(string uId, string confirmationCode)
    {
        UserIdentity userIdentity = await _userManager.FindByIdAsync(uId);
        Guard.Against.Null(userIdentity);
        
        EmailConfirmationCode? code = _db.EmailConfirmationCodes
            .FirstOrDefault(
                e => e.Email.Value == userIdentity.Email);
        
        if (code is null)
        {
            throw new AppValidationException("Code not found");
        }

        if (code.ConfirmationCode.Code != confirmationCode)
        {
            throw new AppValidationException("Invalid code");
        }
        
        userIdentity.EmailConfirmed = true;

        await _userManager.UpdateAsync(userIdentity);

        await RemoveCodeFromDb(code);
    }

    public async Task RemoveCodeFromDb(EmailConfirmationCode code)
    {
        _db.EmailConfirmationCodes.Remove(code);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsCodeValid(EmailConfirmationCode code)
    {
        EmailConfirmationCode? codeModel = 
            await _db.FindAsync<EmailConfirmationCode>(code);

        if (codeModel is not null)
        {
            return true;
        }

        return false;
    }

    private void RemoveOldCodeIfExist(string email)
    {
        var code = _db.EmailConfirmationCodes.FirstOrDefault(
            e => e.Email.Value == email);

        if (code is not null)
        {
            _db.EmailConfirmationCodes.Remove(code);
        }
    }
}
