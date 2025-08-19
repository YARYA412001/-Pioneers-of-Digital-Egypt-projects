using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public class Courses
    {
        public string Title { get; }
        public string Description { get; set; }
        public int MaximumDegree { get; }
        public List<Exam> Exams { get; } = new List<Exam>();

        public Courses(string title, string description, int max)
        {
            Title = title;
            Description = description;
            MaximumDegree = max;
        }

        public bool WouldExceedMaxDegree(Exam newExam)
        {
            decimal currentTotal = Exams.Sum(exam => exam.TotalDegree());
            return currentTotal + newExam.TotalDegree() > MaximumDegree;
        }

        public void AddExam(params Exam[] exams)
        {
            foreach (Exam exam in exams)
            {
                if (WouldExceedMaxDegree(exam))
                {
                    Console.WriteLine($"Cannot add exam '{exam.Name}' - would exceed maximum course degree.");
                }
                else if (Exams.Any(e => e.Name == exam.Name))
                {
                    Console.WriteLine($"Exam with name '{exam.Name}' already exists in this course.");
                }
                else
                {
                    Exams.Add(exam);
                }
            }
        }

        public Exam FindExam(string examName)
        {
            return Exams.FirstOrDefault(e => e.Name.Equals(examName, StringComparison.OrdinalIgnoreCase));
        }

        public void ShowExams()
        {
            Console.WriteLine($"Exams in course '{Title}':");
            foreach (Exam exam in Exams)
            {
                Console.WriteLine($"- {exam.Name} (Total Marks: {exam.TotalDegree()})");
            }
        }
    }
}
