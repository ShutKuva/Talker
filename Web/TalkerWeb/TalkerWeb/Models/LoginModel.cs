using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace TalkerWeb.Models
{
    public class LoginModel : 
    {
        [Required(ErrorMessage = "Будь ласка, введіть свій логін")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть пароль")]
        [MaxLength(32, ErrorMessage = "Забагато літер")]
        [MinLength(8, ErrorMessage = "Замало літер")]
        [RegularExpression("^[a-zA-Z\\d\\\\\\[\\]_\\*\\-!]*$", ErrorMessage = "Не валідний пароль")]
        public string Password { get; set; }
    }
}
