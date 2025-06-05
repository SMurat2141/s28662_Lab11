using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
        Task AddUserAsync(User user, CancellationToken ct);
        Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct);
        Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}