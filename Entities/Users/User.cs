using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }

        public override bool Equals(object? obj)
        {
            User user = obj as User;
            if (user is null) 
                return false;

            return Username == user.Username && Password == user.Password;
        }
    }

    public enum UserType
    {
        Admin = 1, 
        Instructor,
        Student
    }
}
