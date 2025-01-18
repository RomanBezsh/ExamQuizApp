using ExamQuizApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.Managers
{
    public static class UserManager
    {
        private static List<User> _users = new List<User>();
        public static void Register(string login, string password, DateTime date)
        {
            if (_users.Any(user => user.Login == login))
            {
                throw new Exception("Данный логин уже занят");
            }
            _users.Add(new User { Login = login, Password = password, DateOfBirth = date });
            FileManager.SaveUsers(_users);
        }
        public static User Login(string login, string password)
        {
            var user = _users.FirstOrDefault(user => user.Login == login && user.Password == password);
            if (user == null)
                throw new Exception("Данного аккаунта нет");
            return user;
        }
        public static void LoadUsers()
        {
            _users = FileManager.LoadUsers();
        }
    }
}
