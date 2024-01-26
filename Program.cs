using BankingSystem.Contracts;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BankingSystem
{
    //driver program
    class Program
    {
            

        static void logger(string msg) => Console.WriteLine(msg);
       
        static void Main()
        {
            BankingSystem bankingSystem = 
                new BankingSystem(logger);

            bankingSystem.OnAccountAlert += (sender, eventArgs) =>
            Console.WriteLine(eventArgs.Message);

            try {
                MyBankProgram(bankingSystem);


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MyBankProgram(bankingSystem);

            }




        }

        static void MyBankProgram(BankingSystem bankingSystem) {

            try
            {

          
            while (true)
            {

                //label


                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create new account");
                Console.WriteLine("2. Deposit money");
                Console.WriteLine("3. Withdraw money");
                Console.WriteLine("4. Print account details");
                Console.WriteLine("5. Exit");

                 

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateAccount(bankingSystem);
                            break;
                        case 2:
                            DepositMoney(bankingSystem);
                            break;
                        case 3:
                            WithdrawMoney(bankingSystem);
                            break;
                        case 4:
                            PrintAccountDetails(bankingSystem);
                            break;
                        case 5:
                            Console.WriteLine("Exiting the program.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }
            }
            catch
            {
                throw;
            }
        }


        static void CreateAccount(BankingSystem bankingSystem)
        {
            Console.Write("Enter account holder's name: ");
            string accountHolder = Console.ReadLine(); //account name

            Console.Write("Enter initial balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                bankingSystem.CreateAccount(accountHolder, initialBalance);
                Console.WriteLine("Account created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input for initial balance. Account creation failed.");
            }
        }

        static bool TryParseDecimal(string input, out decimal result)
        {
            if (Regex.IsMatch(input, @"^[0-9]+$"))
            {
                // If the string contains only numbers, try parsing it as decimal
                return decimal.TryParse(input, out result);
            }
            else
            {
                result = 0; // Default value if parsing fails
                return false;
            }
        }

        static void DepositMoney(BankingSystem bankingSystem)
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.Write("Enter deposit amount: ");
                var parseResult = TryParseDecimal(Console.ReadLine(), out decimal result);
                bankingSystem.Deposit(accountNumber, result);

                if (!parseResult)
                    Console.WriteLine("input not accepted ");

            }
            else
            {
                Console.WriteLine("Invalid input for account number.");
            }
        }

        static void WithdrawMoney(BankingSystem bankingSystem)
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.Write("Enter withdrawal amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
                {
                    bankingSystem.Withdraw(accountNumber, withdrawalAmount);
                }
                else
                {
                    Console.WriteLine("Invalid input for withdrawal amount.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for account number.");
            }
        }

        static void PrintAccountDetails(BankingSystem bankingSystem)
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                bankingSystem.PrintAccountDetails(accountNumber);
            }
            else
            {
                Console.WriteLine("Invalid input for account number.");
            }
        }
    }
}