using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces.Validators
{
    public interface IPasswordValidator
    {
        public string IsItValidPassword(string password);
    }
}
