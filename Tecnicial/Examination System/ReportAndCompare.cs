using Examination_System;

public class ReportAndCompare
{
    public static void GenerateStudentReport(Student student)
    {
        Console.WriteLine($"REPORT FOR STUDENT: {student.Name}");
        Console.WriteLine("==========================================");

        student.ShowExamResults();

        Console.WriteLine("==========================================");
    }

    public static void CompareStudents(Student student1, Student student2, string courseTitle, string examName)
    {
        string resultKey = $"{courseTitle}_{examName}";

        bool hasResult1 = student1.ExamResults.TryGetValue(resultKey, out decimal score1);
        bool hasResult2 = student2.ExamResults.TryGetValue(resultKey, out decimal score2);

        Console.WriteLine($"COMPARISON FOR EXAM: {examName} IN COURSE: {courseTitle}");
        Console.WriteLine("==========================================");

        if (!hasResult1 && !hasResult2)
        {
            Console.WriteLine("Neither student has taken this exam.");
        }
        else if (!hasResult1)
        {
            Console.WriteLine($"{student1.Name} has not taken this exam.");
            Console.WriteLine($"{student2.Name}'s score: {score2}");
        }
        else if (!hasResult2)
        {
            Console.WriteLine($"{student2.Name} has not taken this exam.");
            Console.WriteLine($"{student1.Name}'s score: {score1}");
        }
        else
        {
            Console.WriteLine($"{student1.Name}: {score1}");
            Console.WriteLine($"{student2.Name}: {score2}");

            if (score1 > score2)
                Console.WriteLine($"{student1.Name} scored higher by {score1 - score2} points.");
            else if (score2 > score1)
                Console.WriteLine($"{student2.Name} scored higher by {score2 - score1} points.");
            else
                Console.WriteLine("Both students scored the same.");
        }

        Console.WriteLine("==========================================");
    }
}


