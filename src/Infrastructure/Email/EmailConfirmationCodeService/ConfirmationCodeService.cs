using Application.Interfaces.Services.Email;
using Ardalis.GuardClauses;
using Synword.Domain.Entities.Identity;
using Synword.Domain.Entities.Identity.ValueObjects;
using Synword.Infrastructure.Identity;

namespace Infrastructure.Email.EmailConfirmationCodeService;

public class ConfirmationCodeService : IConfirmationCodeService
{
    private readonly AppIdentityDbContext _db;

    public ConfirmationCodeService(AppIdentityDbContext db)
    {
        Guard.Against.Null(db);
        
        _db = db;
    }
    
    public async Task<EmailConfirmationCode> CreateNew(string email)
    {
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

        return codeModel;
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
