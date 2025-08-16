using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class Account
    {
        private static int nextAccountNumber = 1000;  // Auto-generate starting from 1000

        public int AccountNumber { get; }
        public double Balance { get; protected set; }
        public DateTime DateOpened { get; }
        public List<Transaction> TransactionHistory { get; } = new List<Transaction>();

        protected Account()
        {
            AccountNumber = nextAccountNumber++;
            Balance = 0;
            DateOpened = DateTime.Now;
        }

        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                TransactionHistory.Add(new Transaction("Deposit", 0));
            }

            else
            {
                Balance += amount;
                TransactionHistory.Add(new Transaction("Deposit", amount));
            }
        }

        public abstract void Withdraw(double amount);  // To be implemented in subclasses

        public void Transfer(Account toAccount, double amount)
        {
            Withdraw(amount);  // This will validate based on account type
            toAccount.Deposit(amount);
            TransactionHistory.Add(new Transaction("Transfer Out", -amount));
            toAccount.TransactionHistory.Add(new Transaction("Transfer In", amount));
        }

        public void ShowTransactionHistory()
        {
            Console.WriteLine($"Transaction History for Account {AccountNumber}:");
            foreach (var trans in TransactionHistory)
            {
                Console.WriteLine(trans);
            }
        }
    }
}
