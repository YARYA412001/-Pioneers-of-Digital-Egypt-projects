namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create bank at start
            var bank = new Bank("YahyaBank", "Yahya001");
            // Example usage (add your menu logic here)
            var customer1 = new Customer("Yahya 1", "30112041701835", new DateTime(2001, 12, 4));
            bank.AddCustomer(customer1);
            var customer2 = new Customer("Yahya 2", "30112041701865", new DateTime(2001, 12, 4));
            bank.AddCustomer(customer2);
            var savings = new SavingsAccount(0.05);  // 5% interest
            bank.AddAccountToCustomer(customer1.Id, savings);
            var current = new CurrentAccount(500);  // $500 overdraft
            bank.AddAccountToCustomer(customer1.Id, current);
            savings.Deposit(1000);
            savings.Withdraw(200);
            savings.CalculateMonthlyInterest();
            current.Deposit(500);
            current.Withdraw(700);  // Uses overdraft
            savings.Transfer(current, 300);
            Console.WriteLine($"Total balance for customer {customer1.FullName}: {customer1.GetTotalBalance():C}");
            savings.ShowTransactionHistory();
            bank.ShowBankReport();
            // Search example
            var searchResults = bank.SearchCustomers("Yahya");
            foreach (var cust in searchResults)
            {
                Console.WriteLine($"Found: {cust.FullName}");
            }
            // Update example
            Console.WriteLine($"Customer before uddated \nName : {customer1.FullName}\nbirthedate: {customer1.DateOfBirth}");
            bank.UpdateCustomer(customer1.Id, "Yahya Updated", new DateTime(2001, 4, 12));
            Console.WriteLine($"Customer after uddated \nName : {customer1.FullName}\nbirthedate: {customer1.DateOfBirth}");
            // Remove (only if balances zero - adjust balances first if testing)
             bank.RemoveCustomer(customer1.Id);
            bank.RemoveCustomer(customer2.Id);

        }
    }
}
    


