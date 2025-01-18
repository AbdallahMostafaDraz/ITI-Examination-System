using Entities.Exams;
using Entities.Helpers;
using Entities.Persons;
using Entities.Questions;
using Entities.Subjects;
using Entities.Users;

namespace App
{
    internal class Handler
    {
        // Admin Page
        public static void HandleAdminPage()
        {
            // If First Time No Check but save first admin
            User adminUser = Helper.ReadLoginData();
            if (!File.Exists(Paths.AdminsUsersFilePath))
                Helper.SaveToFile(Paths.AdminsUsersFilePath, adminUser);

            // check if user data is valid

            Printer.DiplayLogInOut("Logging in");

            if (!Helper.isValidAdmin(Paths.AdminsUsersFilePath, adminUser))
            {

                Printer.DisplayInvalidUser();
                return;
            }


            while (true)
            {
                Printer.DisplayAdminMenu();
                string choice = Console.ReadLine();
                if (choice == "8")
                {
                    Printer.DiplayLogInOut("Logging out");
                    Printer.DisplayMainMenu();
                    break;
                }

                HandleAdminChoice(choice);
            }
        }

        static void HandleAdminChoice(string choice)
        {

            switch (choice)
            {
                case "1":
                    // Add New Admin
                    User user = Helper.ReadLoginData();
                    Helper.SaveToFile(Paths.AdminsUsersFilePath, user);
                    break;
                case "2":
                    // Add New Subject
                    Subject subject = SubjectHelper.ReadSubjectData();
                    Helper.SaveToFile(Paths.SubjectsFilePath, subject);
                    break;
                case "3":
                    // Show All Subjects
                    PrintFileData<Subject>(Paths.SubjectsFilePath, "No Subjects Added Yet!");
                    break;
                case "4":
                    // Add New Instructor
                    Instructor instructor = InstructorHelper.ReadInstructorData(Paths.SubjectsFilePath);
                    Helper.SaveToFile(Paths.InstructrorsFilePath, instructor);
                    break;
                case "5":
                    // Show All Instructors 
                    PrintFileData<Instructor>(Paths.InstructrorsFilePath, "No Instructors Added Yet!");
                    break;
                case "6":
                    // Add New Student
                    HandleAddStudent();
                    break;
                case "7":
                    // Show All Students
                    PrintFileData<Student>(Paths.StudentsFilePath, "No Students Added Yet!");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Invalid Choice. Please Try Again");
                    break;
            }
            Printer.DisplayContinue();

        }

        static void HandleAddStudent()
        {
            Student student = StudentHelper.ReadStudentData(Paths.SubjectsFilePath);
            Helper.SaveToFile(Paths.StudentsFilePath, student);

            var exams = GetExamsForStudentSubjects(student.Subjects);

            foreach (Exam exam in exams)
            {
                exam.OnExamCreated += student.AssignInExam;
                exam.CreateExam();
            }

        }
        static void PrintFileData<T>(string filePath, string emptyMessage)
        {
            if (!File.Exists(filePath))
                Console.WriteLine(emptyMessage);
            else
                Helper.Print(Helper.GetAll<T>(filePath));
        }

        static List<Exam> GetExamsForStudentSubjects(IEnumerable<Subject> subjects)
        {
            var allPracticeExams = Helper.GetAll<Exam>(Paths.PracticeExamsFilePath);
            var allFinalExams = Helper.GetAll<Exam>(Paths.FinalExamsFilePath);
            var examsInSubjects = new List<Exam>();

            foreach (var exam in allPracticeExams.Concat(allFinalExams))
            {
                if (subjects.Contains(exam.Subject))
                    examsInSubjects.Add(exam);
            }

            return examsInSubjects;
        }

        // =================

        // Instructor Page
        public static void HandleInstructorLogin()
        {
            User instructorUser = Helper.ReadLoginData();
            Instructor validInstructor = InstructorHelper.isValidInstructor(Paths.InstructrorsFilePath, instructorUser);


            Printer.DiplayLogInOut("Logging in");

            if (validInstructor == null)
            {

                Printer.DisplayInvalidUser();
                return;
            }


            while (true)
            {
                Printer.DisplayInstructorMenu();
                string choice = Console.ReadLine();

                if (choice == "5")
                {
                    Printer.DiplayLogInOut("Logging out");
                    break;
                }

                HandleInstructorChoice(choice, validInstructor);
            }

        }


        static void HandleInstructorChoice(string choice, Instructor instructor)
        {
            switch (choice)
            {
                case "1":
                    HandleAddQuestions(instructor);
                    break;
                case "2":
                    HandleCreateExam(instructor);
                    break;
                case "3":
                    HandleStartExam(instructor);
                    break;
                case "4":
                    HandleShowResults(instructor);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        
        // to get subject what user select from instructor's subject to add questions and exams in it
        static Subject GetSubjectToAddInIt(Instructor instructor)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n Choose One Subject Of Yours: ");
            Helper.Print(instructor.Subjects);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Subejct Id: ");
            int subjectId = int.Parse(Console.ReadLine());
            Subject subject = Helper.GetAll<Subject>(Paths.SubjectsFilePath).FirstOrDefault(e => e.Id == subjectId);
            return subject;
        }


        static void HandleAddQuestions(Instructor instructor)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            var subject  = GetSubjectToAddInIt(instructor);
            string choice = "";

            while (choice != "4")
            {

                Console.WriteLine("\n Please Choose Type Of Question: ");
                foreach (int val in Enum.GetValues(typeof(QuestionHeader)))
                    Console.WriteLine($" [{val}] {(QuestionHeader)val}");
                Console.WriteLine(" [4] Back");
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write("\n Choice: ");
                choice = Console.ReadLine();
                string again = "y";
                switch (choice)
                {
                    case "1":
                        while (again == "y")
                        {
                            
                            TrueFalseQuestion tfQuestion = QuestionHelper.ReadTFQuestionData(subject);
                            Helper.SaveToFile(Paths.TRQuestionsFilePath, tfQuestion);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Add New Questoin (y/n)? ");
                            again = Console.ReadLine();
                            if (again == "y")
                                Printer.ClearSpecificPostoin(8);
                            else 
                                Printer.ClearSpecificPostoin(18);
                        }
                        break;
                    case "2":
                        while (again == "y")
                        {
                            ChooseOneQuestion coQuestion = QuestionHelper.ReadChooseOneQuestionData(subject);
                            Helper.SaveToFile(Paths.ChooseOneQuestionFilePath, coQuestion);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Add New Questoin (y/n)? ");
                            again = Console.ReadLine();
                            if (again == "y")
                                Printer.ClearSpecificPostoin(11);
                            else
                                Printer.ClearSpecificPostoin(19);
                        }
                        break;
                    case "3":
                        while (again == "y")
                        {
                            int numberofCorrectAnswers = 0; 
                            ChooseAllQuestion caQuestion = QuestionHelper.ReadChooseAllQuestionData(subject, out numberofCorrectAnswers);
                            Helper.SaveToFile(Paths.ChooseAllQuestionFilePath, caQuestion);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" Add New Questoin (y/n)? ");
                            again = Console.ReadLine();
                            if (again == "y")
                                Printer.ClearSpecificPostoin(11 + numberofCorrectAnswers);
                            else
                                Printer.ClearSpecificPostoin(19 + numberofCorrectAnswers);
                        }
                        break;
                    case "4":
                        choice = "4";
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Printer.DisplayContinue();
                        break;
                }
            }
        }

        static void HandleCreateExam(Instructor instructor)
        {
            Subject subject = GetSubjectToAddInIt(instructor);

            
            Exam exam = ExamHelper.ReadExamData(subject.Id);

            Console.Write("\n Save Exam (y/n)? ");
          
            if (Console.ReadLine()?.ToLower() == "y")
            {
                if (exam.ExamType == ExamType.Practice)
                    Helper.SaveToFile(Paths.PracticeExamsFilePath, exam);
                else
                    Helper.SaveToFile(Paths.FinalExamsFilePath, exam);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" Note: ");
                foreach (var student in StudentHelper.GetStudentsInSubject(Paths.StudentsFilePath, subject.Id))
                    exam.OnExamCreated += student.AssignInExam;

                exam.CreateExam();
                Printer.DisplayContinue();
            }
        }

        static void HandleStartExam(Instructor instructor)
        {

            Subject subject = GetSubjectToAddInIt(instructor);


            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n Choose Type Of Exam: ");
            foreach (int val in Enum.GetValues(typeof(ExamType)))
                Console.WriteLine($" [{val}] {(ExamType)val}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Exam Type: ");
            int examTypeSelected = int.Parse(Console.ReadLine());


            string path = examTypeSelected == 1 ? Paths.PracticeExamsFilePath : Paths.FinalExamsFilePath;

            var exams = Helper.GetAll<Exam>(path).Where(e => e.Subject.Id == subject.Id).ToList();
            Helper.Print(exams);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Enter Exam ID: ");
            int examId = int.Parse(Console.ReadLine());
            Exam selectedExam = exams.FirstOrDefault(e => e.Id == examId);

            foreach (var student in StudentHelper.GetStudentsInSubject(Paths.StudentsFilePath, subject.Id))
                selectedExam.OnExamStart += student.StartExam;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            selectedExam.StartExam();

            Console.ForegroundColor = ConsoleColor.Blue;
            Printer.DisplayContinue();
        }

        static void HandleShowResults(Instructor instructor)
        {
            Subject subject = GetSubjectToAddInIt(instructor);

            var studentsInSubject = StudentHelper.GetStudentsInSubject(Paths.StudentsFilePath, subject.Id);


            var allExamsInfo = Helper.GetAll<ExamInfo>(Paths.ExamsInfoFilePath)
                                        .Where(e => studentsInSubject.Any(s => s.Id == e.StudentId));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n All Student's Results in exams of {subject!.Name}");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var result in allExamsInfo)
            {
                Console.WriteLine($" Student Id [{result.StudentId}]: " +
                    $" {result.Name} - Result ({result.StudentScore} / {result.ExamScore})");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Printer.DisplayContinue();
        }

        // ========================

        // Student Page
        public static void HandleStudentLogin()
        {
            User studentUser = Helper.ReadLoginData();
            Student validStudent = StudentHelper.isValidStudent(Paths.StudentsFilePath, studentUser);

            Printer.DiplayLogInOut("Logging in");



            if (validStudent == null)
            {
                Printer.DisplayInvalidUser();

                return;
            }

            Console.WriteLine("\nWelcome To Students Page");
            Console.WriteLine("----------------------------------");

            while (true)
            {
                Printer.DisplayStudentMenu();
                string choice = Console.ReadLine();

                if (choice == "4")
                {
                    Printer.DiplayLogInOut("Logging out");
                    break;
                }
                HandleStudentChoice(choice, validStudent);
            }
        }

        
        static void HandleStudentChoice(string choice, Student student)
        {
            switch (choice)
            {
                case "1":
                    HandleStartStudentExam(student, ExamType.Practice);
                    break;
                case "2":
                    HandleStartStudentExam(student, ExamType.Final);
                    break;
                case "3":
                    HandleShowStudentMarks(student);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void HandleStartStudentExam(Student student, ExamType examType)
        {
            var exams = StudentHelper.GetStudentExams(Paths.StudentsFilePath, student.Id, examType);
            if (!exams.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" No Exams Available!");
                Console.ForegroundColor = ConsoleColor.Blue;
                Printer.DisplayContinue();
                return;
            }

            Helper.Print(exams);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Enter Exam ID: ");
            int examId = int.Parse(Console.ReadLine());

            Exam selectedExam = exams.FirstOrDefault(e => e.Id == examId);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Printer.CenterText("\n-----------------------------------------------------------------------------------------------------------------------", ConsoleColor.Magenta);
            
            Printer.CenterText($"[[ {selectedExam!.Name} ]]\n", ConsoleColor.Magenta);
            Printer.CenterText($"Subject: {selectedExam.Subject.Name}  |  Date: {selectedExam.Date.ToString("dd/MM/yyyy")}  |  Duration: {selectedExam.Duration} Hours ", ConsoleColor.Magenta);
            Printer.CenterText("-----------------------------------------------------------------------------------------------------------------------", ConsoleColor.Magenta);



            ExamInfo result = ExamHelper.LoadExamAndCorrect(selectedExam);

            Console.Write("\n Submit Exam (y/n)? ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                result.StudentId = student.Id;
                Helper.SaveToFile(Paths.ExamsInfoFilePath, result);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" --- Your Score: {result.StudentScore} / {result.ExamScore} ---");
            }

            Printer.DisplayContinue();
        }

        static void HandleShowStudentMarks(Student student)
        {
            var results = Helper.GetAll<ExamInfo>(Paths.ExamsInfoFilePath)
                .Where(r => r.StudentId == student.Id).ToList();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n Your Exams Results:");
            Console.WriteLine("----------------------");
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($" [{i + 1}]: [{results[i].Name}]");
                Console.WriteLine($" Score: {results[i].StudentScore} / {results[i].ExamScore}");
                Console.WriteLine("-----------------------------------");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            Printer.DisplayContinue();
        }

    }
}
