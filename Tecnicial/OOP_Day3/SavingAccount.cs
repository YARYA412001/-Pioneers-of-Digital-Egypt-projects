using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Day3
{
    internal class SavingAccount : BankAccount
    {
        public float InterestRate { get; set; }

        public SavingAccount(string holderName, float balance, float interestRate)
            : base(holderName, balance)
        {
            InterestRate = interestRate;
        }

        public override float CalculateInterest()
        {
            return Balance * InterestRate / 100;
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Interest Rate: {InterestRate}%");
        }
    }
}
