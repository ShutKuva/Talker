using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PresentationLayer.Validators
{
    public class PasswordValidator
    {
        public string IsItValidPassword(string password)
        {
            int maxLen = 24;
            int minLen = 8;
            if (password.Length < minLen)
            {
                return $"Password can not be shorter than {minLen} characters";
            }
            if (password.Length > maxLen)
            {
                return $"Password can not be shorter than {maxLen} characters";
            }
            if (!IsInItAllValidSymbols(password))
            {
                return "No valid symbols";
            }
            return null;
        }

        bool IsInItAllValidSymbols(string password)
        {
            string regex = @"^\w+\d+[\.\[\{\(\*\+\?\^\$\|_]*$";
            return Regex.IsMatch(password, regex);
        }
    }
}
