using OOP_Day1and2;

class Program
{
    static void Main(string[] args)
    {
        BankAccount acc1 = new BankAccount();
        BankAccount acc2 = new BankAccount("Yahya Ramadan", "30112041701835", "01007215557", "Menofia", 5000f);
        BankAccount acc3 = new BankAccount("Yossef Ramadan", "11111111111111", "01007215557", "Menofia");

        acc1.ShowAccountDetails();
        acc2.ShowAccountDetails();
        acc3.ShowAccountDetails();
    }
}
