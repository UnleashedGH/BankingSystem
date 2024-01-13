using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Contracts
{
    public interface IBankingSystem
    {

        void CreateAccount(string accountHolder, decimal initialBalance);
        void Deposit(int accountNumber, decimal amount);
        void Withdraw(int accountNumber, decimal amount);

        void PrintAccountDetails(int accountNumber);
    }
}
