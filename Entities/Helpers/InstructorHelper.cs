using Entities.Persons;
using Entities.Subjects;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helpers
{
    public class InstructorHelper : Helper
    {
        public static Instructor isValidInstructor(string filePath, User user)
        {
            var instructor = GetAll<Instructor>(filePath).FirstOrDefault(e => e.UserName == user.Username && e.Password == user.Password);
            if (instructor is not null)
                return instructor;
            return default;
        }
        public static Instructor ReadInstructorData(string filePath)
        {
            Instructor instructor = new Instructor();
            Console.Write("\n Instructor ID:   ");
            instructor.Id = int.Parse(Console.ReadLine());
            Console.Write(" Instructor Name: ");
            instructor.Name = Console.ReadLine();
            Console.Write(" Username:        ");
            instructor.UserName = Console.ReadLine();
            Console.Write(" Password:        ");
            instructor.Password = ReadPassword();
            Console.Write("\n Do You Want Add Subjects To This Instrcutor(y/n)? ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                List<Subject> listOfSubjects = new List<Subject>();
                var allSubjects = GetAll<Subject>(filePath);
                do
                {
                    Print(allSubjects);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" Subject ID: ");
                    int subjectId = int.Parse(Console.ReadLine());
                    var subjectToAdd = allSubjects.Find(e => e.Id == subjectId);
                    instructor.Subjects.Add(subjectToAdd);
                    Console.Write(" Add Another Subject(y/n)?: ");
                    choice = Console.ReadLine();
                } while (choice.ToLower() == "y");
            }
            return instructor;
        }
    }
}
