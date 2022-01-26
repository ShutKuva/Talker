using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces.Validators;
using Core;
using Microsoft.Extensions.Options;

namespace BLL.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        PasswordValidationParameters _passwordValidationParameters;

        public PasswordValidator(IOptions<PasswordValidationParameters> passwordValidationParameters)
        {
            _passwordValidationParameters = passwordValidationParameters?.Value ?? throw new ArgumentNullException(nameof(passwordValidationParameters));
        }

        public string IsItValidPassword(string password)
        {
            if (password.Length < _passwordValidationParameters.MinimumNumberOfCharacters)
            {
                return $"Password can not be shorter than {_passwordValidationParameters.MinimumNumberOfCharacters} characters";
            }
            if (password.Length > _passwordValidationParameters.MaximumNumberOfCharacters)
            {
                
                return $"Password can not be shorter than {_passwordValidationParameters.MaximumNumberOfCharacters} characters";
            }
            if (!IsInItAllValidSymbols(password, _passwordValidationParameters))
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
