using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Day1and2
{
    internal class BankAccount
    {
        public const string BankCode = "BNK001"; 
        public readonly DateTime CreatedDate;
        private static int _accountNumber=1;
        private string _fullName;
        private string _nationalID;
        private string _phoneNumber;
        private string _address;
        private float _balance;
        
        public int AccountNumber { get; private set; }
        public string FullName
        {
            get
            {  
                return _fullName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("Full name must not be empty.");
                else
                    _fullName = value;
            }
        }
        public string NationalID
        {
            get
            { 
                return _nationalID;
            } 
            set
            {
                if (value.Length != 14 )
                    Console.WriteLine("National ID must be exactly 14 digits.");
                else
                    _nationalID = value;
            }
        }
        public string PhoneNumber
        {
            get 
            {
                return _phoneNumber;
            }
            set
            {
                if (value.Length != 11 || !value.StartsWith("01") )
                    Console.WriteLine("Phone number must start with '01' and be 11 digits.");
                else
                    _phoneNumber = value;
            }
        }
        public string Address
        {
            get
            { 
                return _address; 
            }
            set
            {
                _address = value; 
            }
        }
        public float Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                if (value < 0)
                    Console.WriteLine("Balance must be >= 0.");
                else
                    _balance = value;
            }
        }
        public BankAccount()
        {
            AccountNumber = _accountNumber++;
            Console.WriteLine("Hello please enter you information ");
            Balance = 1;
            CreatedDate = DateTime.Now;

        }
        public BankAccount(string fullName, string nationalID, string phoneNumber, string address, float balance)
        {
            AccountNumber = _accountNumber++;
            CreatedDate = DateTime.Now;
            FullName = fullName;
            NationalID = nationalID;
            PhoneNumber = phoneNumber;
            Address = address;
            Balance = balance;
        }

        public BankAccount(string fullName, string nationalID, string phoneNumber, string address)
        {
            AccountNumber = _accountNumber++;
            CreatedDate = DateTime.Now;
            FullName = fullName;
            NationalID = nationalID;
            PhoneNumber = phoneNumber;
            Address = address;
            Balance = 1000; 
        }
        // Methods
        public void ShowAccountDetails()
        {
            Console.WriteLine("-----------------------------Account Details -----------------------------");
            Console.WriteLine($"Bank Code: {BankCode}");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"National ID: {NationalID}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Balance: {Balance} EGP");
            Console.WriteLine($"Created Date: {CreatedDate}");
            //Console.WriteLine(AccountNumber);
            Console.WriteLine("********************************************************************");
            Console.WriteLine();
        }
        public bool IsValidNationalID()
        {
            return NationalID.Length == 14 ;
        }
        public bool IsValidPhoneNumber()
        {
            return PhoneNumber.Length == 11 && PhoneNumber.StartsWith("01");
        }


    }
}
