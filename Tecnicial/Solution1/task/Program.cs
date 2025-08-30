namespace task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double, double> addtion = (x, y) => x + y;
            Func<double, double, double> subtract = (x, y) => x - y;
            Func<double, double, double> multiply = (x, y) => x * y;
            Func<double, double, double> divide = (x, y) => x / y ;
            Console.WriteLine("Please enter the opertion you want");
            Console.WriteLine("1-Addtion");
            Console.WriteLine("2-Subtract");
            Console.WriteLine("3-Multiply");
            Console.WriteLine("4-Divide");
            Console.Write("The number of opertion you want :");
            int op = int.Parse(Console.ReadLine());
            Console.Write("Please enter first number :");
            double x=Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter second number :");
            double y = Convert.ToDouble(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.WriteLine($"{x} + {y} = {x.calc(y, addtion)}");
                    break;
                case 2:
                    Console.WriteLine($"{x} - {y} = {x.calc(y, subtract)}");
                    break;
                case 3:
                    Console.WriteLine($"{x} * {y} = {x.calc(y, multiply)}");
                    break;
                case 4:
                    Console.WriteLine($"{x} / {y} = {x.calc(y, divide)}");
                    break;
                default:
                    Console.WriteLine("from 1 to 4 only");
                    break;
            }


        }
    }
}
