namespace Entities.Questions
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion()
        {
            Header = QuestionHeader.TrueFalse;
            Answers = new List<Answer>()
            {
                new Answer {Id = 1, Body = "True"},
                new Answer {Id = 2, Body = "False"},
            };
        }
        
    }
}
