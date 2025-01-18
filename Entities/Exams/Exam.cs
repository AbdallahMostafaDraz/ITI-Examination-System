using Entities.Questions;
using Entities.Subjects;

namespace Entities.Exams
{
    public  class Exam 
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public DateTime Date { get; set; } 
        public int Duration { get; set; }
        public List<Question> Questions { get; set; } = new();
        public ExamType ExamType { get; set; } 
        public Subject Subject { get; set; } 
        public int Marks { get; set; } 
        
        public event Action<Exam> OnExamCreated;

        public  event Action<Exam> OnExamStart;

        public void StartExam()
        {
            OnExamStart?.Invoke(this);
        }

        public void CreateExam()
        {
            OnExamCreated?.Invoke(this);
        }

        public override string ToString()
        {
            return $"[{Id}]: {Name}";
        }
    }
}