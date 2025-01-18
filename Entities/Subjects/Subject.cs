using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Subjects
{
    public class Subject 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        public override bool Equals(object? obj)
        {
            Subject other = obj as Subject;
            if (other == null) 
                return false;
            
            return Id == other.Id  && Name == other.Name;
        }
    }
}
