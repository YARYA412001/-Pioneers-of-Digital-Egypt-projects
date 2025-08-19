using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{


    public class MultipleChoice : Question
    {
        public List<string> Options { get; set; } = new List<string>();

        public MultipleChoice(string text, decimal mark, List<string> options, int correct) : base(text, mark)
        {
            Options = options;
            CorrectAnswer = correct;
            ValidCorrect(correct);
        }

        public void ValidCorrect(int correct)
        {
            if (correct < 1 || correct > Options.Count)
                throw new Exception("Correct answer must be within the option range");
        }

        public override void display()
        {
            Console.WriteLine(Text + $" Marks: {Marks}");
            for (int i = 0; i < Options.Count; i++)
                Console.WriteLine($"    {i + 1}. {Options[i]}");
        }
    }
}
