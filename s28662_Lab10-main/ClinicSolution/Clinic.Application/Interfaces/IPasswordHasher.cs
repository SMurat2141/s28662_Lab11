namespace Clinic.Application.Interfaces
{
    public interface IPasswordHasher
    {
        (byte[] hash, byte[] salt) Hash(string password);
        bool Verify(string password, byte[] hash, byte[] salt);
    }
}