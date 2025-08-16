using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Customer
    {
        private static int nextId = 1;  // Auto-generate IDs

        public int Id { get; }
        public string FullName { get; private set; }
        public string NationalId { get; }
        public DateTime DateOfBirth { get; private set; }
        public List<Account> Accounts { get; } = new List<Account>();
         private void ValidNationalId(string nid)
        {
            if (string.IsNullOrWhiteSpace(nid))
                throw new ArgumentException("NationalId cannot be empty.");
            if (nid.Length != 14)
                throw new ArgumentException("NationalId must be 14 digits");
            if (!(nid.All(char.IsDigit)))
                throw new ArgumentException("NationalId must be number");
        }

        public Customer(string fullName, string nationalId, DateTime dateOfBirth)
        {
            ValidateHelber.Name(fullName);
            ValidNationalId(nationalId);
            Id = nextId++;
            FullName = fullName;
            NationalId = nationalId;
            DateOfBirth = dateOfBirth;
        }

        public void UpdateDetails(string newName, DateTime newDob)
        {
            ValidateHelber.Name(newName);
            FullName = newName;
            DateOfBirth = newDob;
        }

        public double GetTotalBalance()
        {
            double total = 0;
            foreach (var account in Accounts)
            {
                total += account.Balance;
            }
            return total;
           
        }
        

        public bool CanBeRemoved()
        {
            foreach (var account in Accounts)
            {
                if (account.Balance != 0)
                    return false;
            }
            return true;
        }

    }
}
