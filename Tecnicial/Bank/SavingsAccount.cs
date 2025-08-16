using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class SavingsAccount : Account
    {
        public double InterestRate { get; }  // e.g., 0.05 for 5%

        public SavingsAccount(double interestRate)
        {
            InterestRate = interestRate;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                TransactionHistory.Add(new Transaction("Withdraw", 0));
            }
            else if (amount > Balance)
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

        public double CalculateMonthlyInterest()
        {
            double interest = Balance * (InterestRate / 12);
            Deposit(interest);  // Add interest as a deposit
            TransactionHistory.Add(new Transaction("Interest", interest));
            return interest;
        }
    }
}
