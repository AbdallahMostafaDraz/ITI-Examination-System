
using Entities.Subjects;

namespace Entities.Questions
{
    public  class Question
    {
        public int Id { get; set; }
        public QuestionHeader Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public List<Answer> Answers { get; set; } = new();
        public Subject Subject { get; set; } = new();

        public List<int> CorrectAnswers { get; set; } = new();

        public override string ToString()
        {
            return $" [{Id}] {Body}\t\t({Marks}) Marks";
        }
    }
}
