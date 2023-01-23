using System.Security.Cryptography;

namespace Utilities.Helpers
{
    public static class HashHelper
    {
        public static (byte[] passwordSalt, byte[] passwordHash) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
