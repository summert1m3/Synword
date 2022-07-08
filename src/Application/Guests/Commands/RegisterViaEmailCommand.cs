using Application.Exceptions;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces.Repository;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Email.EmailService;

namespace Application.Guests.Commands;

public class RegisterViaEmailCommand : IRequest
{
    public RegisterViaEmailCommand(
        string email,
        string password,
        string uId)
    {
        Email = email;
        Password = password;
        UId = uId;
    }
    
    public string Email { get; }
    public string Password { get; }
    public string UId { get; }
}

internal class RegisterViaEmailCommandHandler 
    : IRequestHandler<RegisterViaEmailCommand>
{
    private readonly ISynwordRepository<User> _userRepository;
    private readonly UserManager<AppUser>? _userManager;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly IEmailService _emailService;
    
    public RegisterViaEmailCommandHandler(
        ISynwordRepository<User> userRepository,
        UserManager<AppUser>? userManager,
        IPasswordHasher<AppUser> passwordHasher,
        IEmailService emailService
        )
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
    }
    
    public async Task<Unit> Handle(
        RegisterViaEmailCommand request, 
        CancellationToken cancellationToken)
    {
        AppUser? identityUser = 
            await _userManager!.FindByIdAsync(request.UId);
        Guard.Against.Null(identityUser);
        
        if (await IsUserAlreadyHaveEmail(identityUser, cancellationToken))
        {
            throw new AppValidationException("UserAlreadyHaveEmail");
        }

        identityUser.Email = request.Email;
        identityUser.PasswordHash = 
            _passwordHasher.HashPassword(identityUser, request.Password);
        identityUser.SecurityStamp = Guid.NewGuid().ToString();

        await ChangeRole(identityUser);

        await _userManager.UpdateAsync(identityUser);
        
        await SendConfirmationEmail(request.Email);

        return Unit.Value;
    }

    private async Task<bool> IsUserAlreadyHaveEmail(
        AppUser identityUser,
        CancellationToken cancellationToken)
    {
        if (identityUser.Email is not null)
        {
            return true;
        }

        return false;
    }

    private async Task SendConfirmationEmail(string email)
    {
        await _emailService.SendConfirmationEmailAsync(email);
    }

    private async Task ChangeRole(AppUser identityUser)
    {
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());
    }
}
