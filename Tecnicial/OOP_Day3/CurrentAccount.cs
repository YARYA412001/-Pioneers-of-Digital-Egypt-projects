using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Day3
{
    internal class CurrentAccount : BankAccount
    {
        public float OverdraftLimit { get; set; }

        public CurrentAccount(string holderName, float balance, float overdraftLimit)
            : base(holderName, balance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override float CalculateInterest()
        {
            return 0; 
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Overdraft Limit: {OverdraftLimit}");
        }
    }

}
