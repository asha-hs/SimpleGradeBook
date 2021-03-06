using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }
    public delegate void GradeAddedDelegate(object sender,EventArgs args);

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        event GradeAddedDelegate GradeAdded;
        string Name { get;}
    }
    public abstract class Book : NamedObject,IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
        
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            Name = name;
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
          using(var writer = File.AppendText($"{Name}.txt"))
          {
               writer.WriteLine(grade);
               if(GradeAdded != null)
               {
                   GradeAdded(this,new EventArgs());
               }
          }
      
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
           
            
           using(var reader = File.OpenText($"{Name}.txt"))
           {
               var line = reader.ReadLine();
               while(line != null)
               {
                   var number = double.Parse(line);
                   result.Add(number);
                   line = reader.ReadLine();
                  
               }
           }          
            
            return result;
        }
    }
    public class InMemoryBook : Book
    {
        private  List<double> grades;
       public const string CATEGORY = "Science";

        public override event GradeAddedDelegate GradeAdded;

        

        public InMemoryBook(string name) : base(name)
        {
            Name = name;
            grades = new List<double>();
        }
       
       public void AddLetterGrade(char letter)
       {
           switch(letter)
           {
               case 'A' :
                AddGrade(90);
                break;
                case 'B':
                AddGrade(80);
                break;
                case 'C':
                AddGrade(70);
                break;
                default:
                AddGrade(0);
                break;
           }
       }
        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                 grades.Add(grade);
                 if(GradeAdded != null)
                 {
                     GradeAdded(this, new EventArgs());
                 }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
           
        }

        public override Statistics GetStatistics()
        {
            
            var result = new Statistics();
           
            
           for(var index = 0; index < grades.Count; index++)
           {
               result.Add(grades[index]);
               
            
           };
            
            
            return result;
         
                
        }
    }

    
}
