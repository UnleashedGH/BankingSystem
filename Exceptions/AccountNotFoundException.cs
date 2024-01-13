using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Account Was Not Found")
        {
             
        }
    }
}
