using MediatR;
using Clinic.Application.DTOs;

namespace Clinic.Application.Commands.Auth
{
    public record LoginUserCommand(LoginDto Dto) : IRequest<(string accessToken, string refreshToken)>;
}