using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Exceptions;

namespace BankingSystem
{
    public sealed  class Account
    {
        private static int accountCounter = 1;
        public int AccountNumber { get; }


        public string AccountHolder { get; }
        //assume we are dealing in USD
        public decimal  Balance { get; set; }

        public Account(string accountHolder, decimal initialBalance)
        {
            AccountNumber = accountCounter++;
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }


        public void Deposit(decimal amount) => Balance += amount;

        public void Withdraw(decimal amount)
        {
            if (CheckBalance(amount)) Balance -= amount;
         
        }

        private bool CheckBalance(decimal amount)
        {
            var suffientBalance = Balance > amount;
            if (!suffientBalance) throw new NonSufficentBalance();
            return suffientBalance;
        }






    }



}
