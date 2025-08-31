using auth.Resources;
using auth.Services;

namespace auth.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }

        public static User Create(string email, string userName, string password, string pepper, int iteration)
        {
            var passwordSalt = PasswordHasher.GenerateSalt();
            var passwordHash = PasswordHasher.HashPassoword(password, passwordSalt, pepper, iteration);
            return new User
            {
                Email = email,
                UserName = userName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };
        }
    }
}
