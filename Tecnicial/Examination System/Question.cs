using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public abstract class Question
    {
        private decimal mark;

        public string Text { get; set; }
        public decimal Marks { get; set; }

        public int  CorrectAnswer{ get; set; }


        protected Question(string text, decimal mark)
        {
            Text = text;
            Marks = mark;
        }
        public abstract void display();

    }
}
