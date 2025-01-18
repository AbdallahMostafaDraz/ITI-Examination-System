using Entities.Exams;
using Entities.Persons;
using Entities.Questions;
using Entities.Subjects;

namespace Entities.Helpers
{
    public class ExamHelper : Helper
    {

        public static Exam ReadExamData(int subjectId)
        {
            Exam exam;

            // Print All Types Of Exams
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n Choose Type Of Exam: ");
            foreach (int val in Enum.GetValues(typeof(ExamType)))
                Console.WriteLine($" [{val}] {(ExamType)val}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Exam Type: ");
            int examTypeSelected = int.Parse(Console.ReadLine());

            exam = examTypeSelected == (int)ExamType.Practice ? new PracticeExam() : new FinalExam();

            // read exam data
            Console.Write("\n Exam ID:   ");
            exam.Id = int.Parse(Console.ReadLine());

            Console.Write(" Exam Name: ");
            exam.Name = Console.ReadLine();

            Console.Write(" Date:      ");
            exam.Date = DateTime.Parse(Console.ReadLine());

            Console.Write(" Duration:  ");
            exam.Duration = int.Parse(Console.ReadLine());

            exam.Subject = GetAll<Subject>(Paths.SubjectsFilePath).FirstOrDefault(e => e.Id == subjectId)!;

            string choice = "";
            int qusetionId;
            Question questionToAdd;
            while (choice != "4")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n Choose Type Of Questions: ");
                foreach (int val in Enum.GetValues(typeof(QuestionHeader)))
                    Console.WriteLine($" [{val}] {(QuestionHeader)val}");
                Console.WriteLine(" [4] End");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" Questions Type: ");
                choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        // load all tfQuestions of subject
                        while(choice.ToLower() != "n")
                        {
                            var tfQuestions = GetAll<TrueFalseQuestion>(Paths.TRQuestionsFilePath)
                            .Where(e => e.Subject.Id == subjectId).ToList();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Print(tfQuestions);
                            
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Enter Question Id To Add In Exam: ");
                            qusetionId = int.Parse(Console.ReadLine());

                            // add question to exam
                            questionToAdd = tfQuestions.FirstOrDefault(e => e.Id == qusetionId);
                            exam.Questions.Add(questionToAdd);
                            exam.Marks += questionToAdd.Marks;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Question Added To Exam Successfully!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("\n Do You Want to add aonther True False Questoin (y/n)? ");
                            choice = Console.ReadLine();
                        }
                        break;
                    case "2":
                        // load all chooseOneQuestion of subject
                        while (choice.ToLower() != "n")
                        {
                            var chooseOneQuestions = GetAll<ChooseOneQuestion>(Paths.ChooseOneQuestionFilePath)
                            .Where(e => e.Subject.Id == subjectId).ToList();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Print(chooseOneQuestions);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Enter Question Id To Add In Exam: ");
                            qusetionId = int.Parse(Console.ReadLine());

                            // add question to exam
                            questionToAdd = chooseOneQuestions.FirstOrDefault(e => e.Id == qusetionId);
                            exam.Questions.Add(questionToAdd);
                            exam.Marks += questionToAdd.Marks;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Question Added To Exam Successfully");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Do You Want to add aonther Choose One Questoin (y/n)? ");
                            choice = Console.ReadLine();
                        }
                        break;
                    case "3":
                        // load all chooseAllQuestion of subject
                        while (choice.ToLower() != "n")
                        {
                            var chooseAllQuestions = GetAll<ChooseAllQuestion>(Paths.ChooseAllQuestionFilePath)
                            .Where(e => e.Subject.Id == subjectId).ToList();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Print(chooseAllQuestions);

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Enter Question Id To Add In Exam: ");
                            qusetionId = int.Parse(Console.ReadLine());

                            // add question to exam
                            questionToAdd = chooseAllQuestions.FirstOrDefault(e => e.Id == qusetionId);
                            exam.Questions.Add(questionToAdd);
                            exam.Marks += questionToAdd.Marks;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Question Added To Exam Successfully");

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Do You Want to add aonther Choose All Questoin (y/n)? ");
                            choice = Console.ReadLine();
                        }
                        break;
                }
            }
            return exam;
        }
        
        public static void StartExam(Exam exam)
        {
            // find all studetns they have this exam
            var studentsHaveExam = GetAll<Student>(Paths.StudentsFilePath)
                .Where(e => e.Exams.Select(e => e.Id).Contains(exam.Id)).ToList();

            foreach (var student in studentsHaveExam)
            {
              exam.OnExamStart += student.StartExam;
            }

        }

        public static List<ExamInfo>  GetExamsNames(string filePath)
        {
            var allPracticeExams = GetAll<Exam>(filePath)
                                               .Select(e => new ExamInfo{ Id = e.Id , Name = e.Name }).ToList();
            return allPracticeExams;
        }
    
        public static ExamInfo LoadExamAndCorrect(Exam exam)
        {

            ExamInfo examInfo = new ExamInfo();
            examInfo.Id = exam.Id;
            examInfo.Name = exam.Name;
            int totalScore = 0;
            List<int> studentAnswers = new List<int>();
            var tfQuestions = exam.Questions.Where(e => e.Header == QuestionHeader.TrueFalse);
            var chooseOneQuestoins = exam.Questions.Where(e => e.Header == QuestionHeader.ChooseOne);
            var chooseAllQuestions = exam.Questions.Where(e => e.Header == QuestionHeader.ChooseAll);


            if (tfQuestions.Count() > 0)
            {
                Console.WriteLine("\n [1] True Or False Questions: ");
                Console.WriteLine("-------------------------------");

                foreach (var question in tfQuestions)
                {
                    Console.WriteLine($" {question}");
                    foreach (var answer in question.Answers)
                    {
                        Console.Write($"  {answer}\t");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\n  Your Answer: ");
                    studentAnswers.Clear();
                    studentAnswers.Add(int.Parse(Console.ReadLine()));
                    if (studentAnswers[0] == question.CorrectAnswers[0])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("  Great! Correct Answer.");
                        examInfo.StudentScore += question.Marks;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Sorry! Wrong Answer.");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"  Correct Answer is: [{question.CorrectAnswers[0]}]");
                    }
                    examInfo.ExamScore += question.Marks;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine();
                }
            }

            if (chooseOneQuestoins.Count() > 0)
            {
                
                Console.WriteLine("\n [2] Choose One Questions: ");
                Console.WriteLine("----------------------------");

                foreach (var question in chooseOneQuestoins)
                {
                    Console.WriteLine(question);
                    foreach (var answer in question.Answers)
                    {
                        Console.Write($" {answer}\t");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\n Your Answer: ");
                    studentAnswers.Clear();
                    studentAnswers.Add(int.Parse(Console.ReadLine()));
                    if (studentAnswers[0] == question.CorrectAnswers[0])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" Great! Correct Answer.");
                        examInfo.StudentScore += question.Marks;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Sorry! Wrong Answer.");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($" Correct Answer is: [{question.CorrectAnswers[0]}]");
                    } 
                    examInfo.ExamScore += question.Marks;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

            }

            if (chooseAllQuestions.Count() > 0)
            {
                Console.WriteLine("\n [3] Choose All Correct Answers Questions: ");
                Console.WriteLine("--------------------------------------------");

                foreach (var question in chooseAllQuestions)
                {
                    Console.WriteLine(question);
                    foreach (var answer in question.Answers)
                    {
                        Console.Write($" {answer}\t");
                    }
                    studentAnswers.Clear();

                    int numberOfStudentAnswers;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\n\n Number Of Answers You Want Enter: ");
                    numberOfStudentAnswers = int.Parse(Console.ReadLine());

                    for (int i = 0; i < numberOfStudentAnswers; i++)
                    {
                        Console.Write($" Your Answer[{i + 1}]: ");
                        studentAnswers.Add(int.Parse(Console.ReadLine()));
                    }

                    if (studentAnswers.OrderBy(x => x).SequenceEqual(question.CorrectAnswers.OrderBy(x => x)))
                    { 
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" Great! Correct Answer.");
                        examInfo.StudentScore += question.Marks;
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Sorry! Wrong Answer.");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($" Correct Answers Are: ");
                        foreach(var answer in question.Answers)
                            Console.Write($" {answer}\t");
                        Console.WriteLine();

                    }
                    examInfo.ExamScore += question.Marks;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;

                }

            }

            return examInfo;
        }
        
    }
}
