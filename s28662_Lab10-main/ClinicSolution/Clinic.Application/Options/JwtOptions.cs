namespace Clinic.Application.Options
{
    public sealed class JwtOptions
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string Key { get; init; } = string.Empty;
        public int AccessTokenMinutes { get; init; }
        public int RefreshTokenDays { get; init; }
    }
}