using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class WrongInputDataException : Exception
    {
        public WrongInputDataException(string message) : base(message)
        {

        }
    }
}
