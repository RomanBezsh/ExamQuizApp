using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamQuizApp.Domain;




namespace ExamQuizApp
{
    public class Menu
    {
        public void ShowStartMenu()
        {
            while (true)
            {
                Console.WriteLine("Викторина");
                Console.WriteLine("1. Войти");
                Console.WriteLine("2. Авторизоваться");
                Console.WriteLine("3. Выйти");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SignIn();
                        break;
                    case "2":
                        SignUp();
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неправильный выбор. Пожалуйста, попробуйте еще раз");
                        break;
                }
            }
        }
        private void ShowMainMenu(string login)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Пользователь: {login}\n\n\n");
                Console.WriteLine($"=== Главное меню ===");
                Console.WriteLine("1. Начать викторину");
                Console.WriteLine("2. Топ-20 игроков");
                Console.WriteLine("3. Посмотреть результаты");
                Console.WriteLine("4. Настройки профиля");
                Console.WriteLine("5. Выйти");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    
                }
            }
        }
        private void SignUp()
        {
            Console.Clear();
            Console.WriteLine("Введите логин");
            var login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            var password = Console.ReadLine();

            Console.WriteLine("Введите дату рождения");
            var dateOfBirth = DateTime.Parse(Console.ReadLine());

            UserManager.Register(login, password, dateOfBirth);
            Console.WriteLine("Аккаунт был успешно создан");
        }
        private void SignIn()
        {
            Console.Clear();
            Console.WriteLine("Введите логин");
            var login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            var password = Console.ReadLine();
            if (UserManager.Login(login, password))
            {
                ShowMainMenu(login);
            }
            else
            {
                throw new ArgumentException("Не существует такого аккаунта");
            }
        }
        private void StartQuiz()
        {

        }






    }




}
