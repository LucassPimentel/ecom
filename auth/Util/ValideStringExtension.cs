using System.Text.RegularExpressions;

namespace auth.Util
{
    public static class ValideStringExtension
    {
        public static string ValidateEmail(this string email)
        {
            var emailPattern = @"^(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}|(?:\[(?:(?:25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.){3}(?:25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\]))$";

            if (!Regex.IsMatch(email, emailPattern))
                throw new ArgumentException("E-mail inválido.", nameof(email));
            return email;
        }

        public static string ValidateUserName(this string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Nome de usuário não pode ser nulo ou vazio.", nameof(userName));
            if (userName.Length < 2 || userName.Length > 50)
                throw new ArgumentException("Nome de usuário deve ter entre 3 e 50 caracteres.", nameof(userName));
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Nome de usuário só pode conter letras, números e underscores.", nameof(userName));
            return userName;
        }

        public static string ValidatePassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha não pode ser nula ou vazia.", nameof(password));
            if (password.Length < 8 || password.Length > 30)
                throw new ArgumentException("Senha deve ter entre 8 e 30 caracteres.", nameof(password));
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,30}$"))
                throw new ArgumentException("Senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.", nameof(password));
            return password;
        }
    }
}
