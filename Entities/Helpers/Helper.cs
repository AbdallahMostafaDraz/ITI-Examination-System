using Entities.Persons;
using Entities.Questions;
using Entities.Subjects;
using Entities.Users;
using System.Text.Json;

namespace Entities.Helpers
{
    public class Helper
    {
        public static void SaveToFile<T>(string filePath, T item)
        {
            List<T> items;

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                string jsonContent = File.ReadAllText(filePath);
                items = JsonSerializer.Deserialize<List<T>>(jsonContent) ?? new List<T>();
            }
            else
            {
                items = new List<T>();
            }

            items.Add(item);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(items, options);

            File.WriteAllText(filePath, updatedJson);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n {item!.GetType().Name} Saved Successfully!\n");

        }

        public static List<T> GetAll<T>(string filePath)
        {
            try
            {
                List<T> items;

                if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(filePath);
                    items = JsonSerializer.Deserialize<List<T>>(jsonContent) ?? new List<T>();
                }
                else
                {
                    items = new List<T>();
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading Data: {ex.Message}");
                return default;
            }
        }

        public static bool isValidAdmin(string filePath, User user)
        {
            bool isValid = false;
            var allUsers = GetAll<User>(filePath);
            foreach (var admin in allUsers)
            {
                if (user.Equals(admin))
                    isValid = true;
            }
            return isValid;
        }

        public static void Print<T>(List<T> items)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n All {items[0]!.GetType().Name}s: ");
            foreach (T item in items)
            {
                Console.WriteLine($"\t{item}");
            }
        }

        public static User ReadLoginData()
        {
            Console.Write("\n Username: ");
            string userName = Console.ReadLine();
            Console.Write(" Password: ");
            string password = ReadPassword();
            Console.WriteLine();
            return new User { Username = userName, Password = password };
        }

        public static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true); // Read key without displaying it
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Handle backspace
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b"); // Remove the last '*' from console
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    // Add character to password
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter); // Continue until Enter is pressed

            return password;
        }





    }
}
