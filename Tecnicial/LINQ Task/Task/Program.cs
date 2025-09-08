using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace LINQtoObject
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 1 - Display book title and its ISBN.
            var q1 = SampleData.Books.Select(b => new { b.Title, b.Isbn });
            //foreach (var item in q1)
            //{
            //    Console.WriteLine($"Title: {item.Title}\nISBN: {item.Isbn}\n---------------------------------");
            //}
            #endregion
            #region  2-Display the first 3 books with price more than 25.
            var q2 = SampleData.Books.Where(b => b.Price > 25).Take(3);
            //foreach (var b in q2)
            //{
            //    Console.WriteLine(b);
            //} 
            #endregion
            # region 3 - Display Book title along with its publisher name. (Using 2 methods).
            var q3 = SampleData.Books.Select(b => new { b.Title, Publisher = b.Publisher.Name });
            //foreach (var item in q3)
            //{
            //    Console.WriteLine(item);
            //}
            var q4 = (from b in SampleData.Books
                      select new { b.Title, Publisher = b.Publisher.Name }
                     );
            //foreach (var item in q3)
            //{
            //    Console.WriteLine(item);
            //} 
            #endregion
            #region 4 - Find the number of books which cost more than 20.
            int count = SampleData.Books.Where(b => b.Price > 20).Count();
            //Console.WriteLine(count);
            #endregion
            #region 5 - Display book title, price and subject name sorted by its subject name ascending and by its price descending.
            var q6 = SampleData.Books.OrderBy(b => b.Subject.Name).ThenByDescending(b => b.Price).Select(b => new
            {
                b.Title,
                b.Price,
                SubjectName = b.Subject.Name
            });
            //foreach (var item in q6)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion
            #region 6 - Display All subjects with books related to this subject. (Using 2 methods).

            var q7 = from s in SampleData.Subjects
                     join b in SampleData.Books
                     on s.Name equals b.Subject.Name
                     group b by b.Subject.Name into g
                     select new
                     {
                         SubjectName = g.Key,
                         Books = g
                     };
            //foreach (var subject in q7)
            //{
            //    Console.WriteLine($"Subject: {subject.SubjectName}");
            //    foreach (var book in subject.Books)
            //    {
            //        Console.WriteLine($"The book: {book.Title}");
            //    }
            //    Console.WriteLine("------------------------");
            //}
            var q8 = SampleData.Books.GroupBy(b => b.Subject.Name);

            //foreach (var group in q8)
            //{
            //    Console.WriteLine($"Subject: {group.Key}");

            //    foreach (var book in group)
            //    {
            //        Console.WriteLine($"The book: {book.Title}");
            //    }
            //    Console.WriteLine("------------------------");
            //} 
            #endregion
            #region 7 - Try to display book title &price(from book objects) returned from GetBooks Function.

            var q9 = SampleData.GetBooks().Cast<Book>().Select(b => new { b.Title, b.Price });
            //foreach (var item in q9)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion
            #region 8 - Display books grouped by publisher &Subject.
            var q = from b in SampleData.Books
                    group b by new { Publisher = b.Publisher.Name, Subject = b.Subject.Name } into g
                    select new
                    {
                        g.Key.Publisher,
                        g.Key.Subject,
                        Books = g
                    };

            foreach (var group in q)
            {
                Console.WriteLine($"Publisher: {group.Publisher}, Subject: {group.Subject}");
                foreach (var book in group.Books)
                {
                    Console.WriteLine($"    The book: {book.Title}");
                }
                Console.WriteLine("------------------------");
            }
            #endregion
        }
    }
}
