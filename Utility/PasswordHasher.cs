using System.Security.Cryptography;
using System.Text;

namespace BEFS.Utility
{
    public class PasswordHasher
    {
        public static void CreatePasswordHash(string password,
        out byte[] passwordHash,
        out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        }

        public static bool VerifyPassword(string password,
            byte[] storedHash,
            byte[] storedSalt)
        {
            using var hmac = new HMACSHA256(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
