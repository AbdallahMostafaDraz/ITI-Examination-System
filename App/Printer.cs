using System.Runtime.InteropServices;

namespace App
{
    public class Printer
    {
        public static void ClearSpecificPostoin(int numberOfLines)
        {
            // مسح فقط الأسطر الخاصة بالسؤال لإعادة الإدخال
            int currentLineCursor = Console.CursorTop;

            // الرجوع للأعلى لمسح السطور
            Console.SetCursorPosition(0, currentLineCursor - numberOfLines); // عدد الأسطر التي تريد مسحها

            // مسح الأسطر السابقة
            for (int i = 0; i <= numberOfLines; i++) // استبدل الرقم 7 بعدد الأسطر المراد مسحها
            {
                Console.Write(new string(' ', Console.WindowWidth)); // كتابة مسافة لملء السطر
                Console.SetCursorPosition(0, currentLineCursor - numberOfLines + i);
            }

            // إعادة المؤشر إلى بداية القسم
            Console.SetCursorPosition(0, currentLineCursor - numberOfLines - 1);
        }

        public static void DiplayLogInOut(string s)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"\n {s}");
            int i = 0;
            while(i++ < 5)
            {
                Console.Write(".");
                Thread.Sleep(400);
            }
        }

        public static void DisplayInvalidUser()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Invalid Username Or Password!\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Try Again (y/n)? ");
            string choice = Console.ReadLine();
            if (choice == "n")
                Environment.Exit(0);
        }
        public static void DisplayContinue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n Press Any Key To Continue...");
            Console.ReadKey();
        }
        static void DisplayHeader()
        {
            Console.Clear();
            Printer.CenterText("========================================================================================================================", ConsoleColor.Red);
            Printer.CenterText("WELCOME TO ITI EXAMINATION SYSTEM", ConsoleColor.Red);
            Printer.CenterText("========================================================================================================================\n", ConsoleColor.Red);


        }
        public static void DisplayMainMenu()
        {
            DisplayHeader();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n Please Choose From The Following List: ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\n [1]: Login As Admin");
            Console.WriteLine("\n [2]: Login As Instructor");
            Console.WriteLine("\n [3]: Login As Student");
            Console.WriteLine("\n [4]: Exit");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("\n Choice: ");
        }

        public static void DisplayAdminMenu()
        {
            DisplayHeader();

            Console.ForegroundColor = ConsoleColor.Magenta;
            
            Console.WriteLine();
            CenterText($"ADMINSTRATOIN PAGE", ConsoleColor.Magenta);
            
            Console.WriteLine("\n Please Choose From The Following List: ");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("\n [1] Add New Admin");
            Console.WriteLine("\n [2] Add New Subject");
            Console.WriteLine("\n [3] Show All Subject");
            Console.WriteLine("\n [4] Add New Instructor");
            Console.WriteLine("\n [5] Show All Instructors");
            Console.WriteLine("\n [6] Add New Student");
            Console.WriteLine("\n [7] Show All Students");
            Console.WriteLine("\n [8] Log Out");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("\n Choice: ");
        }

        public static void DisplayInstructorMenu()
        {
            DisplayHeader();

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            CenterText($"INSTRUCTORS PAGE", ConsoleColor.Magenta);
            
            Console.WriteLine("\n Please Choose One Of The Following List: ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("\n [1] Add New Questions");
            Console.WriteLine("\n [2] Create an Exam");
            Console.WriteLine("\n [3] Start an Exam");
            Console.WriteLine("\n [4] Show All Student's Results");
            Console.WriteLine("\n [5] Log Out");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n Choice: ");

        }

        public static void DisplayStudentMenu()
        {
            DisplayHeader();

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            CenterText($"STUDENTS PAGE", ConsoleColor.Magenta);

            Console.WriteLine("\n Please Choose One Of The Following List: ");
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("\n [1] Start Practice Exam");
            Console.WriteLine("\n [2] Start Final Exam");
            Console.WriteLine("\n [3] Show All My Marks");
            Console.WriteLine("\n [4] Log Out");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n Choice: ");
        }

        public static void CenterText(string text, ConsoleColor color = ConsoleColor.White)
        {
            
            
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            int consoleWidth = Console.WindowWidth;
            int textStartX = Math.Max(0, (consoleWidth - text.Length) / 2);

            Console.SetCursorPosition(textStartX, Console.CursorTop);

            Console.WriteLine(text);

            Console.ForegroundColor = originalColor;
        }


    }

}
