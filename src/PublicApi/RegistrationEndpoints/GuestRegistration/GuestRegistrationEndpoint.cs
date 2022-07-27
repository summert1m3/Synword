using System.Net;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Application.Guests.Commands;
using Synword.Application.Guests.DTOs;
using Synword.Application.Guests.Services;

namespace Synword.PublicApi.RegistrationEndpoints.GuestRegistration;

public class GuestRegistrationEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GuestRegistrationDto>
{
    private readonly IGuestService _guestService;

    public GuestRegistrationEndpoint(IGuestService guestService)
    {
        _guestService = guestService;
    }
    
    [HttpPost("guestRegister")]
    [SwaggerOperation(
        Tags = new[] { "Registration" }
    )]
    public override async Task<ActionResult<GuestRegistrationDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        IPAddress? ip = HttpContext.Connection.RemoteIpAddress;

        if (ip == null)
        {
            return BadRequest();
        }

        GuestRegistrationDto id = await _guestService.RegisterNewGuest(
            ip.ToString(), cancellationToken);
        
        return Ok(id);
    }
}
