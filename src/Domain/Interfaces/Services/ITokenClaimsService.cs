﻿using Synword.Domain.Entities.TokenAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface ITokenClaimsService
{
    public Task<string> GenerateAccessToken(string uId);
    public Task<RefreshToken> GenerateRefreshToken(
        string uId, string deviceId);

    public void ValidateJwtToken(string token);
}
