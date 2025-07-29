using System.Runtime.InteropServices;

namespace Day_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double FirstNumber = 0, SecondNumber = 0, Result = 0;
            char Operator = 'a';
            char n;
            bool k = true;
            Console.WriteLine("Hello!");
            while (true)
            {

                cal(FirstNumber, SecondNumber, Operator, Result);
                Console.WriteLine("Do you want to perform another calculation? Press 'N' for a new operation or any other key to exit.");
                n = Convert.ToChar(Console.ReadLine());
                if (n == 'N' || n == 'n')
                    k = true;
                else
                    break;
            }
        }
        static void cal(Double FirstNumber, double SecondNumber, char Operator, double Result)
        {
            bool key = false;
            do
            {
                key = false;
                Inputs(ref FirstNumber, ref SecondNumber, ref Operator);
                switch (Operator)
                {

                    case 'A':
                        Result = Add(FirstNumber, SecondNumber);
                        print(Result);
                        break;
                    case 'S':
                        Result = Subtract(FirstNumber, SecondNumber);
                        print(Result);
                        break;
                    case 'M':
                        Result = Multiply(FirstNumber, SecondNumber);
                        print(Result);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        Console.WriteLine("please Enter inputs again");
                        key = true;
                        break;
                }
            } while (key);
        }
        static void Inputs(ref Double FirstNumber, ref double SecondNumber, ref char Operator)
        {
            Console.Write("Input the first number: ");
            FirstNumber = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input the first number: ");
            SecondNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("What do you want to do with those numbers ? \n[A]dd\n[S]ubtract\n[M]ultiply");
            Operator = Convert.ToChar(Console.ReadLine());

        }
        static double Add(double a, double b)
        {
            return a + b;
        }
        static double Subtract(double a, double b)
        {
            return a - b;
        }
        static double Multiply(double a, double b)
        {
            return a * b;
        }
        static void print(double result)
        {
            Console.WriteLine($"The result = {result}");
        }
    }
}
