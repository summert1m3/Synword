using System.Security.Claims;
using Application.Users.DTOs;
using Application.Users.Queries;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.UserEndpoints;

public class BalanceEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<BalanceDTO>
{
    private readonly IMediator _mediator;
    
    public BalanceEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("balance")]
    [Authorize]
    public override async Task<ActionResult<BalanceDTO>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        string uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        BalanceDTO response = await _mediator.Send(
            new GetBalanceQuery(uId), cancellationToken);

        return Ok(response);
    }
}
