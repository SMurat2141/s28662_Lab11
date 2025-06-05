using Clinic.Application.Commands.Auth;
using Clinic.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct)
        {
            await _mediator.Send(new RegisterUserCommand(dto), ct);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var tokens = await _mediator.Send(new LoginUserCommand(dto), ct);
            return Ok(new { accessToken = tokens.accessToken, refreshToken = tokens.refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto dto, CancellationToken ct)
        {
            var accessToken = await _mediator.Send(new RefreshTokenCommand(dto.RefreshToken), ct);
            return Ok(new { accessToken });
        }
    }
}