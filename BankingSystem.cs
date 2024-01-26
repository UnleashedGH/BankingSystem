using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Contracts;
using BankingSystem.Exceptions;

namespace BankingSystem
{

    public class CustomAccountAlertEventArgs : EventArgs
    {
        public string Message;

        public CustomAccountAlertEventArgs(string message)
        {
            Message = message;
        }

     
    }

    // manager class (mr hadi)
    public sealed partial class BankingSystem: IBankingSystem
    {

        private List<Account> _accounts;
        private Action<string> _logger;

        //parameterless constructor
        public BankingSystem() {
            _accounts = new List<Account>();

            //empty delegate
            _logger = (str) => { };
        }

        //parameterized constructor
        public BankingSystem(Action<string> logger)
        {
            _accounts = new List<Account>();
            _logger = logger;
        }    
        //parameterized constructor
        public BankingSystem(Action<string> logger, Action<string> logger2)
        {
            _accounts = new List<Account>();
            _logger = logger;
           
        }
        //copy constructor
        public BankingSystem(BankingSystem bs)
        {
            _accounts = bs._accounts;   
        }



        public event EventHandler<CustomAccountAlertEventArgs> OnAccountAlert;


        
    
        public void CreateAccount(string accountHolder, decimal initialBalance)
        {
            var account = new Account(accountHolder, initialBalance);
            account.OnLowBalance += (sender, eventArgs) => OnAccountAlert?.Invoke(sender, 
                new CustomAccountAlertEventArgs(eventArgs.Message));
            account.OnReceivedAmountNegativeOrZero += (sender, eventArgs) => OnAccountAlert?.Invoke(sender,
                new CustomAccountAlertEventArgs(eventArgs.Message));
            _accounts.Add(account);
            
        }

   
      
        public void Deposit(int accountNumber, decimal amount)
        {
            //if (amount <= 0.0m) return;
            GetAccountFor(accountNumber).ThenDepositBy(amount);
     

            //_logger($"Deposited {amount:C} to account {accountNumber}.");

            return;
        }

        public void Withdraw(int accountNumber, decimal amount) =>
            GetAccountFor(accountNumber).ThenWithdrawBy(amount);
      
        public void PrintAccountDetails(int accountNumber)
        {
            var account = GetAccount(accountNumber);

            _logger($"Account Number: {account.AccountNumber}, Holder: {account.AccountHolder}, Balance: {account.Balance:C}");
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
