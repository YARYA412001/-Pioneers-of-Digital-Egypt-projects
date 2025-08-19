using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public abstract class Person
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; }
        public List<Courses> Courses { get; } = new List<Courses>();

        protected Person(string name)
        {
            Id = nextId++;
            Name = name;
        }

        public abstract void ShowCourses();

        public void AddCourse(params Courses[] courses)
        {
            foreach (Courses course in courses)
            {
                if (Courses.Any(c => c.Title == course.Title))
                {
                    Console.WriteLine($"Already enrolled in course: {course.Title}");
                }
                else
                {
                    Courses.Add(course);
                }
            }
        }

        protected Courses FindCourse(string title)
        {
            return Courses.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
