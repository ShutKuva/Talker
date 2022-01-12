using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GuardClauses
{
    public class Guard
    {
        public void CheckStrict(Exception exception, params bool[] allConditions)
        {
            foreach (bool condition in allConditions)
            {
                if (!condition)
                {
                    throw exception;
                }
            }
        }

        public void CheckNoStrict(Exception exception, params bool[] allConditions)
        {
            bool itsTrue = false;
            foreach (bool condition in allConditions)
            {
                if (condition)
                {
                    itsTrue = true;
                    break;
                }
            }
            if (!itsTrue)
            {
                throw exception;
            }
        }
    }
}
