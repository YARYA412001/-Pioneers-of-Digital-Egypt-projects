using Examination_System;
using static System.Net.Mime.MediaTypeNames;

public class TrueOrFalse : Question
{
    public TrueOrFalse(string text, decimal mark, int correct) : base(text, mark)
    {
        CorrectAnswer = correct;
        ValidCorrect();
    }

    public void ValidCorrect()
    {
        if (CorrectAnswer != 1 && CorrectAnswer != 2)
            throw new Exception("Please enter 1 for true or 2 for false");
    }

    public override void display()
    {
        Console.WriteLine(Text + $" Marks: {Marks}");
        Console.WriteLine(" 1. True");
        Console.WriteLine(" 2. False");
    }
}
