using Clinic.Application.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly ClinicDbContext _db;

        public AuthRepository(ClinicDbContext db) => _db = db;

        public Task<User?> GetByUsernameAsync(string username, CancellationToken ct) =>
            _db.Users.Include(u => u.RefreshTokens)
                     .FirstOrDefaultAsync(u => u.Username == username, ct);

        public Task AddUserAsync(User user, CancellationToken ct) =>
            _db.Users.AddAsync(user, ct).AsTask();

        public Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct) =>
            _db.RefreshTokens.AddAsync(token, ct).AsTask();

        public Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct) =>
            _db.RefreshTokens.Include(t => t.User)
                             .FirstOrDefaultAsync(t => t.Token == token && !t.Revoked, ct);

        public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}