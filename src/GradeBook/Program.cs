using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            IBook book = new DiskBook("Asha's Grade Book");
            book.GradeAdded += OnGradeAdded;
            

            GradeEntered(book);

            // book.AddGrade(89.1);
            // book.AddGrade(38.6);
            // book.AddGrade(23.4);
            // book.AddGrade(89.9);
            // book.AddGrade(78.9);
            // book.AddGrade(98.9);
            var stats = book.GetStatistics();

            Console.WriteLine($"The Book category is {InMemoryBook.CATEGORY}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"Average grade is {stats.Average:N1}");
            Console.WriteLine($"The Letter Grade is {stats.Letter}");
        }

        private static void GradeEntered(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter your grade or 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(Object sender,EventArgs e) 
        {
            Console.WriteLine("Grade was Added");
        }
    }
}
