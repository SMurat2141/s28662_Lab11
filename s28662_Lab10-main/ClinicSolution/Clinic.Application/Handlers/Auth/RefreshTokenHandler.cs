using Clinic.Application.Commands.Auth;
using Clinic.Application.Interfaces;
using Clinic.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Clinic.Application.Handlers.Auth
{
    public sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, string>
    {
        private readonly IAuthRepository _repo;
        private readonly JwtOptions _jwt;

        public RefreshTokenHandler(IAuthRepository repo, IOptions<JwtOptions> jwtOpt)
        {
            _repo = repo;
            _jwt = jwtOpt.Value;
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var rt = await _repo.GetRefreshTokenAsync(request.RefreshToken, ct)
                     ?? throw new InvalidOperationException("Invalid refresh token.");

            if (rt.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException("Refresh token expired.");

            var user = rt.User;
            string newAccessToken = GenerateJwt(user);
            return newAccessToken;
        }

        private string GenerateJwt(Clinic.Domain.Entities.User user)
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