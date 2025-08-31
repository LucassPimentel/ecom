using auth.Util;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace auth.Resources
{
    public class RegisterResource
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public RegisterResource(string userName, string email, string password)
        {
            try
            {
                UserName = userName;
                Email = email;
                Password = password;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
