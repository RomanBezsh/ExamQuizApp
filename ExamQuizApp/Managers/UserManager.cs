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
        public static void ChangePassword(string login, string oldPassword, string newPassword)
        {
            var user = _users.FirstOrDefault(user => user.Login == login && user.Password == oldPassword);
            if (user == null)
                throw new Exception("Неверный логин или пароль");
            user.Password = newPassword;
        }
        public static void ChangeDateOfBirth(string login, DateTime date)
        {
            var user = _users.FirstOrDefault(user => user.Login == login);
            if (user == null)
                throw new Exception("Неверный логин");
            user.DateOfBirth = date;
        }
        public static void DeleteUser(string login)
        {
            var user = _users.FirstOrDefault(user => user.Login == login);
            if (user == null)
                throw new Exception("Неверный логин");
            _users.Remove(user);
        }
        public static void LoadUsers()
        {
            _users = FileManager.LoadUsers();
        }
    }
}
