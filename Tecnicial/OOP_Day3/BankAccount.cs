using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Day3
{
    internal class BankAccount
    {
        protected int _accountNumber;
        protected string _accountHolderName;
        protected float _balance;

        private static int _nextId = 1;

        public int AccountNumber 
        {
            get
            {
                return _accountNumber;
            }
        }
        public string AccountHolderName
        {
            get
            { 
                return _accountHolderName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Invalid account holder name");
                }
                else
                {
                    _accountHolderName = value;
                }
            }
        }
        public float Balance
        {
            get
            { 
                return _balance; 
            }
            set
            {
                if (value < 0)
                    Console.WriteLine("Balance cannot be negative");
                else
                    _balance = value;
            }
        }

       
        public BankAccount()
        {
            _accountNumber = _nextId++;
            Console.WriteLine($"Hello account {AccountNumber} ");
        }

        
        public BankAccount(string holderName, float balance) : this()
        {
            AccountHolderName = holderName;
            Balance = balance;
        }

        public virtual float CalculateInterest()
        {
            return 0;
        }

        public virtual void ShowAccountDetails()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Account Number: {_accountNumber}");
            Console.WriteLine($"Holder Name: {_accountHolderName}");
            Console.WriteLine($"Balance: {_balance}");
        }
    }

    

}
