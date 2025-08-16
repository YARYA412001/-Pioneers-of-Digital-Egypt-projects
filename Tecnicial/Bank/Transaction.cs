using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Transaction
    {
        public string Type { get; }  // e.g., "Deposit", "Withdraw", "Transfer" 
        public double Amount { get; }
        public DateTime Date { get; }

        public Transaction(string type, double amount)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Date}: {Type} of {Amount:C}";
        }
    }
}
