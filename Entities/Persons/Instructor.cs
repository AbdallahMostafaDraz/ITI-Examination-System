namespace Entities.Persons
{
    public class Instructor : Person
    {

        public override string ToString()
        {
            string s = $"[{Id}] {Name}, (Username: {UserName})\n";
            s += $"\tHis Subjects:\n";

            if (Subjects.Count > 0)
            {
                foreach (var subject in Subjects)
                {
                    s += $"\t\t{subject}\n";
                }
            }
            return s ;
        }
    }
}
