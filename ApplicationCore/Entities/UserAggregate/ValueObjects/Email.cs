﻿using System.Net.Mail;
using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

public class Email
{
    public Email(string email)
    {
        Guard.Against.NullOrEmpty(email, nameof(email));
        
        MailAddress.TryCreate(email, out MailAddress? validMail);
        
        Value = email;
    }
    public string Value { get; }
}
