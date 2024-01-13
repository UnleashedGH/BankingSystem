using BankingSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    //Fluent Api
    public static class AccountFluentApiExtentions
    {



        public static void ThenDepositBy(this Account account, decimal amount) =>
            account.Deposit(amount);       
        
        
        public static void ThenWithdrawBy(this Account account, decimal amount) =>
            account.Withdraw(amount);
        

      
       
     
    }
}
