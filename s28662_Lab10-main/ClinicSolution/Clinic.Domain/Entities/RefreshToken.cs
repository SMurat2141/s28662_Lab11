namespace Clinic.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Revoked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}