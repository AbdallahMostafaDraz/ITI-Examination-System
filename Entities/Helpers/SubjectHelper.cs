using Entities.Subjects;

namespace Entities.Helpers
{
    public class SubjectHelper : Helper
    {
        public static Subject ReadSubjectData()
        {
            Subject subject = new Subject();
            Console.Write("\n Subject Id:   ");
            subject.Id = int.Parse(Console.ReadLine());
            Console.Write(" Subject Name: ");
            subject.Name = Console.ReadLine();
            return subject;
        }

    }
}
