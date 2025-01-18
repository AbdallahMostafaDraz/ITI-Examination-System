using System.Runtime.InteropServices;

namespace App
{
    
    internal class Program
    {
        
        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //private const int SW_MAXIMIZE = 3;

        static void Main(string[] args)
        {

            //IntPtr consoleWindow = GetConsoleWindow();
            //if (consoleWindow != IntPtr.Zero)
            //{
            //    ShowWindow(consoleWindow, SW_MAXIMIZE);
            //}

            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            
            string choice = "";
            while (true)
            {
                Printer.DisplayMainMenu();
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Handler.HandleAdminPage();
                        break;
                    case "2": 
                        Handler.HandleInstructorLogin(); 
                        break;
                    case "3": 
                        Handler.HandleStudentLogin();
                       break;        
                }
            }
        }
    }
 }

