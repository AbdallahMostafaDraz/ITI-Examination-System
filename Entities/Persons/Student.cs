using Entities.Exams;
using Entities.Helpers;

namespace Entities.Persons
{
    public class Student : Person
    {
        public int Grade { get; set; }
        public override string ToString()
        {
            string s = $"[{Id}] {Name}, Grade({Grade})\n";
            s += $"\tHis Subjects:\n";

            if (Subjects.Count > 0)
            {
                foreach (var subject in Subjects)
                {
                    s += $"\t\t{subject}\n";
                }
            }
            return s;
        }

        public List<Exam> Exams { get; set; } = new();

        public void StartExam(Exam exam)
        {
            Console.WriteLine($" Student [{Name}] Has Been Notified That Exam [{exam.Name}] Is Started!");
        }

        public void AssignInExam(Exam exam)
        {
            Exams.Add(exam);
            StudentHelper.UpdateStudentExams(Paths.StudentsFilePath, this);
            Console.WriteLine($" Student [{Name}] Has Assigned In Exam [{exam.Name}]");
        }
    }
}
