using MediatR;
using Clinic.Application.DTOs;

namespace Clinic.Application.Commands.Auth
{
    public record RegisterUserCommand(RegisterDto Dto) : IRequest;
}