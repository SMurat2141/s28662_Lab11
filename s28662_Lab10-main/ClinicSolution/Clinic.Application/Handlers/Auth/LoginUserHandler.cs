using Clinic.Application.Commands.Auth;
using Clinic.Application.Interfaces;
using Clinic.Application.Options;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Clinic.Application.Handlers.Auth
{
    public sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, (string accessToken, string refreshToken)>
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordHasher _hasher;
        private readonly JwtOptions _jwt;

        public LoginUserHandler(IAuthRepository repo, IPasswordHasher hasher, IOptions<JwtOptions> jwtOpt)
        {
            _repo = repo;
            _hasher = hasher;
            _jwt = jwtOpt.Value;
        }

        public async Task<(string accessToken, string refreshToken)> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await _repo.GetByUsernameAsync(request.Dto.Username, ct)
                       ?? throw new InvalidOperationException("Invalid credentials.");

            if (!_hasher.Verify(request.Dto.Password, user.PasswordHash, user.PasswordSalt))
                throw new InvalidOperationException("Invalid credentials.");

            string accessToken = GenerateJwt(user);
            string refreshToken = Guid.NewGuid().ToString("N");

            var rt = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays),
                User = user
            };

            await _repo.AddRefreshTokenAsync(rt, ct);
            await _repo.SaveChangesAsync(ct);

            return (accessToken, refreshToken);
        }

        private string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwt.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenMinutes),
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}