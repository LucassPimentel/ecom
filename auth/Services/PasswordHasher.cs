using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace auth.Services
{
    public class PasswordHasher
    {
        public static string HashPassoword(string passoword, string salt, string pepper, int iteration)
        {
            if (iteration <= 0) return passoword;

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var saltedPassword = $"{salt}{passoword}{pepper}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(bytes);

            var hash = Convert.ToBase64String(hashBytes);
            return HashPassoword(hash, salt, pepper, iteration - 1);
        }

        public static string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new Byte[16];
            rng.GetBytes(byteSalt);

            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }

    }
}
