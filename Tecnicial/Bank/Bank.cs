using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Bank
    {
        public string Name { get; }
        public string BranchCode { get; }
        private List<Customer> Customers { get; } = new List<Customer>();

        public Bank(string name, string branchCode)
        {
            ValidateHelber.Name(name);
            ValidateHelber.Name(branchCode);
            Name = name;
            BranchCode = branchCode;
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void UpdateCustomer(int customerId, string newName, DateTime newDob)
        {
            var customer = FindCustomerById(customerId);
            if (customer != null)
            {
                customer.UpdateDetails(newName, newDob);
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        public void RemoveCustomer(int customerId)
        {
            var customer = FindCustomerById(customerId);
            if (customer != null)
            {
                if (customer.CanBeRemoved())
                {
                    Customers.Remove(customer);
                    Console.WriteLine("customer remove sucessfully");
                }
                else
                {
                    Console.WriteLine("Cannot remove customer: accounts still have balance.");
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
       

        public List<Customer> SearchCustomers(string searchTerm)
        {
            List<Customer> result = new List<Customer>();
            foreach (var customer in Customers) 
            {
                if (customer.FullName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    customer.NationalId == searchTerm)
                {
                    result.Add(customer);
                }
            }
            return result;
        }
        private Customer FindCustomerById(int id)
        {
            foreach (var customer in Customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }
        public void AddAccountToCustomer(int customerId, Account account)
        {
            var customer = FindCustomerById(customerId);
            if (customer != null)
            {
                customer.Accounts.Add(account);
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        public void ShowBankReport()
        {
            Console.WriteLine($"Bank Report for {Name} - Branch {BranchCode}");
            foreach (var customer in Customers)
            {
                Console.WriteLine($"Customer: {customer.FullName} (ID: {customer.Id})");
                foreach (var account in customer.Accounts)
                {
                    Console.WriteLine($"  Account {account.AccountNumber}: Balance {account.Balance:C}, Opened {account.DateOpened:D}");
                }
            }
        }

     
    }

}
