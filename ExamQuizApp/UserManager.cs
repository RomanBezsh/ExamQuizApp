using ExamQuizApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp
{
    public static class UserManager
    {
        private static List<User> _users = new List<User>();
        public static void Register(string login, string password, DateTime dateOfBirth)
        {
            if (_users.Exists(u => u.Login == login))
            {
                throw new ArgumentException("Этот логин уже занят");
            }
            _users.Add(new User { Login = login, Password = password, DateOfBirth = dateOfBirth });
        }
        public static bool Login(string login, string password)
        {
            return _users.Exists(u => u.Login == login && u.Password == password);
        }
    }
}
