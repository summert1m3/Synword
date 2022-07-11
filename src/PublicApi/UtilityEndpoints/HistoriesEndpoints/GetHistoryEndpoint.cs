using System.Security.Claims;
using Application.Users.DTOs;
using Application.Users.Queries;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.UtilityEndpoints.HistoriesEndpoints;

public class GetHistoryEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<UserHistoriesDto>
{
    private readonly IMediator _mediator;

    public GetHistoryEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("getHistory")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Utility" }
    )]
    public override async Task<ActionResult<UserHistoriesDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        string uId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        UserHistoriesDto userHistories = 
            await _mediator.Send(
                new GetAllUserHistoriesQuery(uId),
                    cancellationToken);
        
        return Ok(userHistories);
    }
}
