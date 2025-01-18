
namespace Entities.Questions
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $"({Id}) {Body}";
        }
    }
}
