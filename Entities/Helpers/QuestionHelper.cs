using Entities.Questions;
using Entities.Subjects;

namespace Entities.Helpers
{
    public class QuestionHelper : Helper
    {
        public static TrueFalseQuestion ReadTFQuestionData(Subject subject)
        {

            TrueFalseQuestion trueFalseQuestion = new TrueFalseQuestion();
            Console.Write("\n Question ID: ");
            trueFalseQuestion.Id = int.Parse(Console.ReadLine());
            Console.Write(" Question:    ");
            trueFalseQuestion.Body = Console.ReadLine();
            Console.Write(" Marks:       ");
            trueFalseQuestion.Marks = int.Parse(Console.ReadLine());
            Console.Write(" Correct Answer ([1]True / [2]False): ");
            trueFalseQuestion.CorrectAnswers.Add(int.Parse(Console.ReadLine()));
            trueFalseQuestion.Subject = subject;

            return trueFalseQuestion;
        }
        public static ChooseOneQuestion ReadChooseOneQuestionData(Subject subject)
        {
            ChooseOneQuestion question = new ChooseOneQuestion();
            Console.Write("\n Question ID: ");
            question.Id = int.Parse(Console.ReadLine());
            Console.Write(" Question:    ");
            question.Body = Console.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($" Answer[{i + 1}]: ");
                question.Answers.Add(new Answer { Id = i + 1, Body = Console.ReadLine() });
            }
            Console.Write(" Marks: ");
            question.Marks = int.Parse(Console.ReadLine());
            Console.Write(" Correct Answer No: ");
            question.CorrectAnswers.Add(int.Parse(Console.ReadLine()));
            question.Subject = subject;
            return question;
        }
        public static ChooseAllQuestion ReadChooseAllQuestionData(Subject subject, out int numberOfCorrectAnswers)
        {
            numberOfCorrectAnswers = 0;
            ChooseAllQuestion question = new ChooseAllQuestion();
            Console.Write("\n Question ID: ");
            question.Id = int.Parse(Console.ReadLine());
            Console.Write(" Question:    ");
            question.Body = Console.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($" Answer[{i + 1}]:   ");
                question.Answers.Add(new Answer { Id = i + 1, Body = Console.ReadLine() });
            }
            Console.Write(" Marks:       ");
            question.Marks = int.Parse(Console.ReadLine());
            Console.Write(" Number Of Correct Answers: ");
            numberOfCorrectAnswers = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfCorrectAnswers; i++)
            {

                Console.Write($" Correct Answer[{i + 1}]: ");
                question.CorrectAnswers.Add(int.Parse(Console.ReadLine()));
                
            }
            question.Subject = subject;
            return question;
        }

    }
        
}
