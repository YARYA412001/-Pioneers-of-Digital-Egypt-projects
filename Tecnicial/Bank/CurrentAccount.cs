using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class CurrentAccount : Account
    {
        public double OverdraftLimit { get; }

        public CurrentAccount(double overdraftLimit)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                TransactionHistory.Add(new Transaction("Withdraw", 0));
            }
            else if (amount > Balance + OverdraftLimit)
            {
                Console.WriteLine("Insufficient balance.");
                TransactionHistory.Add(new Transaction("Withdraw", 0));
            }
            else
            {
                Balance -= amount;
                TransactionHistory.Add(new Transaction("Withdraw", -amount));
            }

        }
    }
}
