using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PasswordValidationParameters
    {
        public string Regex { get; set; }

        public int MinimumNumberOfCharacters { get; set; }

        public int MaximumNumberOfCharacters { get; set; }
    }
}
