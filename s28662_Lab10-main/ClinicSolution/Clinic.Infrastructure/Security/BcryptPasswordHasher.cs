using Clinic.Application.Interfaces;

namespace Clinic.Infrastructure.Security
{
    public sealed class BcryptPasswordHasher : IPasswordHasher
    {
        public (byte[] hash, byte[] salt) Hash(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (System.Text.Encoding.UTF8.GetBytes(hash), System.Text.Encoding.UTF8.GetBytes(salt));
        }

        public bool Verify(string password, byte[] hash, byte[] salt)
        {
            var hashStr = System.Text.Encoding.UTF8.GetString(hash);
            return BCrypt.Net.BCrypt.Verify(password, hashStr);
        }
    }
}