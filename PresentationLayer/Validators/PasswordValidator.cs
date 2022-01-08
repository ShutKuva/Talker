using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;

namespace PresentationLayer.Validators
{
    public class PasswordValidator
    {
        public string IsItValidPassword(string password, PasswordValidationParameters passwordValidationPararmeters)
        {
            if (password.Length < passwordValidationPararmeters.MinimumNumberOfCharacters)
            {
                return $"Password can not be shorter than {passwordValidationPararmeters.MinimumNumberOfCharacters} characters";
            }
            if (password.Length > passwordValidationPararmeters.MaximumNumberOfCharacters)
            {
                return $"Password can not be shorter than {passwordValidationPararmeters.MaximumNumberOfCharacters} characters";
            }
            if (!IsInItAllValidSymbols(password, passwordValidationPararmeters))
            {
                return "No valid symbols";
            }
            return null;
        }

        bool IsInItAllValidSymbols(string password, PasswordValidationParameters passwordValidationParameters)
        {
            return Regex.IsMatch(password, passwordValidationParameters.Regex);
        }
    }
}
