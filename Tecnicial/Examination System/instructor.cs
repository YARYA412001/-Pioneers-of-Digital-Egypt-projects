using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public class Instructor : Person
    {
        public string Specialization { get; set; }

        public Instructor(string name, string specialization) : base(name)
        {
            Specialization = specialization;
        }

        public override void ShowCourses()
        {
            Console.WriteLine($"Instructor {Name} teaches:");
            foreach (Courses course in Courses)
            {
                Console.WriteLine($"- {course.Title}");
            }
        }

        public void CreateExamForCourse(string courseTitle, Exam exam)
        {
            Courses course = FindCourse(courseTitle);
            if (course == null)
            {
                Console.WriteLine($"Not assigned to course: {courseTitle}");
                return;
            }

            course.AddExam(exam);
            Console.WriteLine($"Exam '{exam.Name}' added to course '{courseTitle}'");
        }

        public Exam DuplicateExam(string courseTitle, string examName, string newExamName)
        {
            Courses course = FindCourse(courseTitle);
            if (course == null)
            {
                Console.WriteLine($"Not assigned to course: {courseTitle}");
                return null;
            }

            Exam originalExam = course.FindExam(examName);
            if (originalExam == null)
            {
                Console.WriteLine($"Exam not found: {examName}");
                return null;
            }

            return originalExam.Duplicate(newExamName);
        }
    }

}
