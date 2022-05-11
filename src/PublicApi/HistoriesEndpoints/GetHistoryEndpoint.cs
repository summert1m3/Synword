using System.Security.Claims;
using Application.Users.DTOs;
using Application.Users.Queries;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.HistoriesEndpoints;

public class GetHistoryEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<UserHistoriesDTO>
{
    private readonly IMediator _mediator;

    public GetHistoryEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("getHistory")]
    [Authorize]
    public async override Task<ActionResult<UserHistoriesDTO>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        string uId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        UserHistoriesDTO userHistories = 
            await _mediator.Send(
                new GetAllUserHistories(uId),
                    cancellationToken);
        
        return Ok(userHistories);
    }
}
