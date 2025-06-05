using MediatR;

namespace Clinic.Application.Commands.Auth
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<string>;
}