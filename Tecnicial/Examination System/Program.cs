namespace Examination_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create courses
            Courses math = new Courses("Mathematics", "Basic math course", 100);
            Courses physics = new Courses("Physics", "Basic physics course", 100);

            // Create exams
            Exam mathMidterm = new Exam("Math Midterm");
            TrueOrFalse mathQ1 = new TrueOrFalse("2 + 2 equals 4", 10, 1);
            MultipleChoice mathQ2 = new MultipleChoice("Solve for x: 2x + 5 = 15", 20,
                new List<string> { "x = 5", "x = 10", "x = 7.5", "x = 5.5" }, 1);

            mathMidterm.AddQuestion(mathQ1, mathQ2);

            Exam physicsFinal = new Exam("Physics Final");
            TrueOrFalse physicsQ1 = new TrueOrFalse("The Earth revolves around the Sun", 15, 1);
            Essay physicsQ2 = new Essay("Explain Newton's Laws of Motion", 35);

            physicsFinal.AddQuestion(physicsQ1, physicsQ2);

            // Add exams to courses
            math.AddExam(mathMidterm);
            physics.AddExam(physicsFinal);

            // Create students
            Student student1 = new Student("Yahya", "yahya@example.com");
            Student student2 = new Student("Yousuf", "yousuf@example.com");

            // Enroll students in courses
            student1.AddCourse(math, physics);
            student2.AddCourse(math);

            // Create instructor
            Instructor instructor = new Instructor("Dr. Karim", "Mathematics");
            instructor.AddCourse(math);

            // Students take exams
            student1.TakeExam("Mathematics", "Math Midterm");
            student1.SubmitAnswers("Mathematics", "Math Midterm", 1, 1);

            student2.TakeExam("Mathematics", "Math Midterm");
            student2.SubmitAnswers("Mathematics", "Math Midterm", 2, 3);

            // Generate reports
            ReportAndCompare.GenerateStudentReport(student1);
            ReportAndCompare.GenerateStudentReport(student2);

            // Compare students
            ReportAndCompare.CompareStudents(student1, student2, "Mathematics", "Math Midterm");

            // Instructor duplicates an exam
            Exam mathMidtermCopy = instructor.DuplicateExam("Mathematics", "Math Midterm", "Math Midterm Copy");
            if (mathMidtermCopy != null)
            {
                math.AddExam(mathMidtermCopy);
                Console.WriteLine("Exam duplicated successfully!");
            }

            // Try to modify an exam after it's started (should fail)
            mathMidterm.UpdateQuestion(0, "Modified question", 15, 1);

            Console.ReadKey();
        }
    }
}
