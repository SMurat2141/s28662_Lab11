using Clinic.Application.Interfaces;
using Clinic.Application.Commands.Auth;
using MediatR;
using Clinic.Domain.Entities;
using Clinic.Application.DTOs;

namespace Clinic.Application.Handlers.Auth
{
    public sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IAuthRepository repo, IPasswordHasher hasher)
        {
            _repo = repo;
            _hasher = hasher;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByUsernameAsync(request.Dto.Username, cancellationToken);
            if (existing is not null)
                throw new InvalidOperationException("Username already taken.");

            var (hash, salt) = _hasher.Hash(request.Dto.Password);
            var user = new User
            {
                Username = request.Dto.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _repo.AddUserAsync(user, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);
        }
    }
}