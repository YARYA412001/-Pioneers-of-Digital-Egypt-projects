namespace OOP_Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SavingAccount saving = new SavingAccount("Ali Ahmed", 5000f, 3.5f);
            CurrentAccount current = new CurrentAccount("Sara Mohamed", 2000f, 1000f);
            BankAccount[] accounts = new BankAccount[2];
            accounts[0] = saving;
            accounts[1] = current;
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i].ShowAccountDetails();
                Console.WriteLine($"Calculated Interest: {accounts[i].CalculateInterest()}");
            }
        }
    }
}
