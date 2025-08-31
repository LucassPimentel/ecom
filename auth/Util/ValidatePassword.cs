namespace auth.Util
{
    public class ValidatePassword
    {
        public static void Validate(string password)
        {
            if (password.Length < 6)
            {
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
            }
            if (!password.Any(char.IsUpper))
            {
                throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.");
            }
            if (!password.Any(char.IsLower))
            {
                throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.");
            }
            if (!password.Any(char.IsDigit))
            {
                throw new ArgumentException("A senha deve conter pelo menos um número.");
            }
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new ArgumentException("A senha deve conter pelo menos um caractere especial.");
            }
        }
    }
}
