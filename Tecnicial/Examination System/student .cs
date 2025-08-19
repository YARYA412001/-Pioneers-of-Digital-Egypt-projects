using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public class Student : Person
    {
        public string Email { get; }
        public Dictionary<string, decimal> ExamResults { get; } = new Dictionary<string, decimal>();

        public Student(string name, string email) : base(name)
        {
            Email = email;
        }

        public override void ShowCourses()
        {
            Console.WriteLine($"Student {Name} is enrolled in:");
            foreach (Courses course in Courses)
            {
                Console.WriteLine($"- {course.Title}");
            }
        }

        public void TakeExam(string courseTitle, string examName)
        {
            Courses course = FindCourse(courseTitle);
            if (course == null)
            {
                Console.WriteLine($"Not enrolled in course: {courseTitle}");
                return;
            }

            Exam exam = course.FindExam(examName);
            if (exam == null)
            {
                Console.WriteLine($"Exam not found: {examName}");
                return;
            }

            if (!exam.IsStarted)
            {
                exam.StartExam();
            }

            exam.ShowExam();
        }

        public void SubmitAnswers(string courseTitle, string examName, params int[] answers)
        {
            Courses course = FindCourse(courseTitle);
            if (course == null)
            {
                Console.WriteLine($"Not enrolled in course: {courseTitle}");
                return;
            }

            Exam exam = course.FindExam(examName);
            if (exam == null)
            {
                Console.WriteLine($"Exam not found: {examName}");
                return;
            }

            exam.RecordStudentAnswers(Id, answers);
            decimal score = exam.CalculateStudentScore(Id);

            // Store result with a unique key (course + exam)
            string resultKey = $"{courseTitle}_{examName}";
            ExamResults[resultKey] = score;

            Console.WriteLine($"Answers submitted. Score: {score}/{exam.TotalDegree()}");
        }

        public void ShowExamResults()
        {
            Console.WriteLine($"Exam results for {Name}:");
            foreach (var result in ExamResults)
            {
                string[] parts = result.Key.Split('_');
                Console.WriteLine($"- {parts[0]} -> {parts[1]}: {result.Value}");
            }
        }
    }
}
