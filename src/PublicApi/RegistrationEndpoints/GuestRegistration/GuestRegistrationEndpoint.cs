﻿using System.Net;
using Application.Guests.Commands;
using Application.Guests.DTOs;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.RegistrationEndpoints.GuestRegistration;

public class GuestRegistrationEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GuestRegistrationDto>
{
    private readonly IMediator _mediator;

    public GuestRegistrationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
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

        GuestRegistrationDto id = 
            await _mediator.Send(
                new RegisterNewGuestCommand(ip), cancellationToken);
        
        return Ok(id);
    }
}
