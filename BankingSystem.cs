using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Contracts;
using BankingSystem.Exceptions;

namespace BankingSystem
{

    // manager class (mr hadi)
    public sealed partial class BankingSystem: IBankingSystem
    {
        private List<Account> _accounts;


        public BankingSystem()
        {
            _accounts = new List<Account>();
        }
     
        public void CreateAccount(string accountHolder, decimal initialBalance)
        {
            var account = new Account(accountHolder, initialBalance);
            _accounts.Add(account);
        }

        public void Deposit(int accountNumber, decimal amount)
        {

            if (amount <= 0.0m) return;
            GetAccountFor(accountNumber).ThenDepositBy(amount);
            Console.WriteLine($"Deposited {amount:C} to account {accountNumber}.");
            return;


        }

        public void Withdraw(int accountNumber, decimal amount)
        {
            if (amount <= 0.0m) return;
            //fluent api call
            GetAccountFor(accountNumber).ThenWithdrawBy(amount);
            Console.WriteLine($"Withdrawn {amount:C} from account {accountNumber}.");

            return;

        }

        public void PrintAccountDetails(int accountNumber)
        {
            var account = GetAccount(accountNumber);
            Console.WriteLine($"Account Number: {account.AccountNumber}, Holder: {account.AccountHolder}, Balance: {account.Balance:C}");
            return;
        }
        //pure

        [Pure]
        public Account GetAccount(int accountNumber)
        {

            var account = _accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
            return account != null ? account : throw new AccountNotFoundException();

        }



    }
}
