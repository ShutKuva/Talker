namespace TalkerWeb.Models
{
    public class PasswordValidationParameters
    {
        public string Regex { get; set; }
        public int MinimumNumberOfCharacters { get; set; }
        public int MaximumNumberOfCharacters { get; set; }

        public PasswordValidationParameters(PasswordValidationParameters original)
        {
            Regex = original.Regex;
            MinimumNumberOfCharacters = original.MinimumNumberOfCharacters;
            MaximumNumberOfCharacters = original.MaximumNumberOfCharacters;
        }
    }
}
