﻿using System.Net.Mail;
using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.UserAggregate.ValueObjects;

public class Email
{
    private Email()
    {
        // required by EF
    }
    
    public Email(string email)
    {
        Guard.Against.NullOrEmpty(email, nameof(email));
        
        bool isValid = MailAddress.TryCreate(email, out MailAddress? validMail);

        if (!isValid)
        {
            throw new Exception("Email is not valid");
        }
        
        Value = email;
    }
    public string Value { get; private set; }
}
