using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Exceptions
{
    public class NonSufficentBalance : Exception
    {
        public NonSufficentBalance() :  base("Non Sufficent Balance for user")
        {
            
        }
    }
}
