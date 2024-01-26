using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Exceptions;

namespace BankingSystem
{
    //DDD Domain Driven Design (Rich Domain, Rich Model)

    //event driven design

    //model (class)

    public class CustomAccountEventArgs : EventArgs
    {
        //event args
        public string Message { get; private set; }
        public bool cancel { get; private set; }

        public CustomAccountEventArgs(string message)
        {
            Message = message;
        }

    }

    public sealed class Account
    {
        private static int accountCounter = 1;

        //private fields
        private decimal _balance;

        public int AccountNumber { get; }

        public string AccountHolder { get; }
        //assume we are dealing in USD
        public decimal Balance
        {
            get => _balance;
            set
            {
                if (_balance != value)
                {
                    HandleBalanceLessThan100Notification();
                    HandleAmountLessThanOrEqualZeroNotification(value);

                    //business logic
                    if (ValueAccepted(value))
                        _balance = value;
                }
            }

        }

        private void HandleAmountLessThanOrEqualZeroNotification(decimal value)
        {
            if (value <= 0)
                OnReceivedAmountNegativeOrZero?.Invoke(null, new CustomAccountEventArgs(
                    "Error: Amount is less than Zero or Equal"));
        }

        private void HandleBalanceLessThan100Notification()
        {
            if (_balance <= 100)
                OnLowBalance?.Invoke(this, new CustomAccountEventArgs(
                    "Wanring: low balance reached!!"));
        }

        public event EventHandler<CustomAccountEventArgs> OnLowBalance;
        public event EventHandler<CustomAccountEventArgs> OnReceivedAmountNegativeOrZero;

        public Account(string accountHolder, decimal initialBalance)
        {
            AccountNumber = accountCounter++;
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        //State modifers, bussiness logic
        public void Deposit(decimal amount) => Balance += amount;
        public void Withdraw(decimal amount) => Balance -= amount;



        private bool ValueAccepted(decimal Value)
        {
            if (Value <= 0) return false;
            return true;

        }





    }


}
