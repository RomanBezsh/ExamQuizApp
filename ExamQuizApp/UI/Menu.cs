using ExamQuizApp.Domain;
using ExamQuizApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.UI
{
    internal class Menu
    {
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Зарегестрироваться");
                Console.WriteLine("2. Войти");
                Console.WriteLine("3. Выйти");
                
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        var loggedUser = Login();
                        ShowMenu(loggedUser);
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        break;
                }
            }
        }
        private void ShowMenu(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Начать викторину");
                Console.WriteLine("2. Посмотреть результаты своих прошлых викторин");
                Console.WriteLine("3. Топ 10 ваших прохождений");
                Console.WriteLine("4. Изменить данные аккаунта");
                Console.WriteLine("5. Топ 10 ваших прохождений");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        return;
                    case "4":
                        return;
                    case "5":
                        return;
                    default:
                        break;
                }
            }
        }
        //Опции в начале программы
        private void Register()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите логин: ");
                    string login = Console.ReadLine();
                    if (login == null)
                        throw new Exception("Логин не может быть пустым.");

                    Console.WriteLine("Введите пароль: ");
                    string password = Console.ReadLine();
                    if (password == null)
                        throw new Exception("Пароль не может быть пустым.");

                    Console.WriteLine("Введите день своего рождения: ");
                    int day = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите месяц своего рождения: ");
                    int month = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите год своего рождения: ");
                    int year = int.Parse(Console.ReadLine());

                    UserManager.Register(login, password, new DateTime(year, month, day));
                    Console.WriteLine("Вы успешно зарегистрировались. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private User Login()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите логин: ");
                    string login = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(login))
                        throw new Exception("Логин не может быть пустым.");

                    Console.WriteLine("Введите пароль: ");
                    string password = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(password))
                        throw new Exception("Пароль не может быть пустым.");

                    var user = UserManager.Login(login, password);
                    Console.WriteLine("Вы успешно вошли в систему. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.");
                }
            }
        }
        private void StartQuiz(User user)
        {
            Console.Clear();
            Console.WriteLine("Выберите викторину за её номером: ");
            var quizzes = QuizManager.GetTitleOfQuizzes();
            int index = 1;
            foreach (var titleOfQuiz in quizzes)
            {
                Console.WriteLine($"{index++}. {titleOfQuiz}");
            }
            int choice = int.Parse(Console.ReadLine());
            if (choice < 1 || choice > quizzes.Count)
                throw new Exception("Неверный номер викторины.");

        }

    }

}
