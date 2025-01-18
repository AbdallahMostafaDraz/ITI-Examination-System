using Entities.Exams;
using Entities.Persons;
using Entities.Subjects;
using Entities.Users;
using System.Text.Json;

namespace Entities.Helpers
{
    public class StudentHelper : Helper
    {
        public static Student ReadStudentData(string filePath)
        {
            Student student = new Student();
            Console.Write("\n Student Id:   ");
            student.Id = int.Parse(Console.ReadLine());
            Console.Write(" Student Name: ");
            student.Name = Console.ReadLine();
            Console.Write(" Grade:        ");
            student.Grade = int.Parse(Console.ReadLine());
            Console.Write(" Username:     ");
            student.UserName = Console.ReadLine();
            Console.Write(" Password:     ");
            student.Password = ReadPassword();

            Console.Write("\n Do You Want Add Subjects To This Student(y/n)? ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                List<Subject> listOfSubjects = new List<Subject>();
                var allSubjects = GetAll<Subject>(filePath);
                do
                {
                    Print(allSubjects);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" Subject Id: ");
                    int subjectId = int.Parse(Console.ReadLine());
                    var subjectToAdd = allSubjects.Find(e => e.Id == subjectId);
                    student.Subjects.Add(subjectToAdd);
                    Console.Write(" Add Another Subject(y/n)?: ");
                    choice = Console.ReadLine();
                } while (choice.ToLower() == "y");
            }

            return student;
        }

        public static Student isValidStudent(string filePath, User user)
        {
            var student = GetAll<Student>(filePath).FirstOrDefault(e => e.UserName == user.Username && e.Password == user.Password);
            if (student is not null)
                return student;
            return default;
        }

        public static void UpdateStudentExams(string filePath, Student student)
        {
            string jsonContent = File.ReadAllText(filePath);
            List<Student> items = JsonSerializer.Deserialize<List<Student>>(jsonContent);
            List<Student> tempItems = new List<Student>(items);
           
            foreach (Student item in items)
            {
                    if (item.Id == student.Id)
                    {
                        tempItems.Remove(item);
                        break;
                    }
            }
                
            

           
            tempItems.Add(student);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(tempItems, options);

            File.WriteAllText(filePath, updatedJson);
        }

        public static List<Student> GetStudentsInSubject(string filePath, int subjectId)
        {
            List<Student> studentsInSubject = new List<Student>();

            var allStudent = GetAll<Student>(filePath);

            foreach (Student student in allStudent)
            {
                foreach (var subject in student.Subjects)
                {
                    if (subject.Id == subjectId)
                    {
                        studentsInSubject.Add(student);
                        break;
                    }    
                }
            }

            return studentsInSubject;
        }

        public static List<Exam> GetStudentExams(string filePath, int stuedntId, ExamType examType)
        {
            var student = GetAll<Student>(filePath).FirstOrDefault(e => e.Id == stuedntId);
            var exams = student!.Exams.Where(e => e.ExamType == examType).ToList();
            return exams;
        }
    }
}
