using Examination_System;

public class Exam
{
    public string Name { get; set; }
    public List<Question> Questions { get; } = new List<Question>();
    public Dictionary<int, List<int>> StudentAnswers { get; } = new Dictionary<int, List<int>>();
    public bool IsStarted { get; private set; }

    public Exam(string name)
    {
        Name = name;
    }

    public decimal TotalDegree()
    {
        return Questions.Sum(question => question.Marks);
    }

    public void AddQuestion(params Question[] questions)
    {
        if (IsStarted)
        {
            Console.WriteLine("Cannot add questions to an exam that has started.");
            return;
        }

        Questions.AddRange(questions);
    }

    public void StartExam()
    {
        IsStarted = true;
    }

    public void RecordStudentAnswers(int studentId, params int[] answers)
    {
        if (!StudentAnswers.ContainsKey(studentId))
        {
            StudentAnswers[studentId] = new List<int>();
        }

        StudentAnswers[studentId].Clear();
        int len = Math.Min(Questions.Count, answers.Length);

        for (int i = 0; i < len; i++)
        {
            StudentAnswers[studentId].Add(answers[i]);
        }
    }

    public decimal CalculateStudentScore(int studentId)
    {
        if (!StudentAnswers.ContainsKey(studentId) || StudentAnswers[studentId].Count == 0)
            return 0;

        decimal score = 0;
        var answers = StudentAnswers[studentId];
        int len = Math.Min(Questions.Count, answers.Count);

        for (int i = 0; i < len; i++)
        {
            if (answers[i] == Questions[i].CorrectAnswer)
            {
                score += Questions[i].Marks;
            }
        }

        return score;
    }

    public void ShowExam()
    {
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Exam: {Name}");
        Console.WriteLine("-----------------------------");

        int count = 1;
        foreach (Question question in Questions)
        {
            Console.Write($"Q{count}. ");
            question.display();
            Console.WriteLine();
            count++;
        }

        Console.WriteLine("-----------------------------");
    }

    public void UpdateQuestion(int index, string text, decimal mark, int correct)
    {
        if (IsStarted)
        {
            Console.WriteLine("Cannot update questions after exam has started.");
            return;
        }

        if (index < 0 || index >= Questions.Count)
        {
            Console.WriteLine("Invalid question index.");
            return;
        }

        if (Questions[index] is TrueOrFalse tfQuestion)
        {
            tfQuestion.Text = text;
            tfQuestion.Marks = mark;
            tfQuestion.CorrectAnswer = correct;
            tfQuestion.ValidCorrect();
        }
        else
        {
            Console.WriteLine("Cannot update - question is not a True/False type.");
        }
    }

    public void UpdateQuestion(int index, string text, decimal mark, int correct, List<string> options)
    {
        if (IsStarted)
        {
            Console.WriteLine("Cannot update questions after exam has started.");
            return;
        }

        if (index < 0 || index >= Questions.Count)
        {
            Console.WriteLine("Invalid question index.");
            return;
        }

        if (Questions[index] is MultipleChoice mcQuestion)
        {
            mcQuestion.Text = text;
            mcQuestion.Marks = mark;
            mcQuestion.CorrectAnswer = correct;
            mcQuestion.Options = options;
            mcQuestion.ValidCorrect(correct);
        }
        else
        {
            Console.WriteLine("Cannot update - question is not a Multiple Choice type.");
        }
    }

    public void RemoveQuestion(int index)
    {
        if (IsStarted)
        {
            Console.WriteLine("Cannot remove questions after exam has started.");
            return;
        }

        if (index >= 0 && index < Questions.Count)
        {
            Questions.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid question index.");
        }
    }

    public Exam Duplicate(string newName)
    {
        Exam newExam = new Exam(newName);

        foreach (Question question in Questions)
        {
            if (question is TrueOrFalse tf)
            {
                newExam.AddQuestion(new TrueOrFalse(tf.Text, tf.Marks, tf.CorrectAnswer));
            }
            else if (question is MultipleChoice mc)
            {
                newExam.AddQuestion(new MultipleChoice(mc.Text, mc.Marks,
                    new List<string>(mc.Options), mc.CorrectAnswer));
            }
            else if (question is Essay es)
            {
                newExam.AddQuestion(new Essay(es.Text, es.Marks));
            }
        }

        return newExam;
    }
}
