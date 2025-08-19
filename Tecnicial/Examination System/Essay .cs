using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public class Essay : Question
    {
        public Essay(string text, decimal marks) : base(text, marks) 
        {
            CorrectAnswer = 1;
        }

        public override void display()
        {
            Console.WriteLine(Text + $" Marks:{Marks}");

            Console.WriteLine(" (Essay - manual check)");
        }
    }
}
