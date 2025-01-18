
namespace Entities.Exams
{
    public class ExamInfo
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int StudentId { get; set; }
        public int ExamScore { get; set; }
        public int StudentScore { get; set; }
        public override string ToString()
        {
            return $"[{Id}]: {Name}";
        }
    }
}
