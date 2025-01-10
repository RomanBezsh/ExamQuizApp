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
        public User? User { get; set; }
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
                        Login();
                        ShowMenu(User);
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        Console.ReadLine();
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
                Console.WriteLine("5. Редактор викторин");
                Console.WriteLine("6. Выйти");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StartQuiz(User);
                        break;
                    case "2":
                        ShowHistory(User);
                        break;
                    case "3":
                        ShowTop10Results(User);
                        break;
                    case "4":
                        ChangeUserData(User);
                        break;
                    case "5":
                        ShowQuizEditor();
                        break;
                    case "6":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        private void ShowQuizEditor()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Создать викторину");
                Console.WriteLine("2. Создать вопрос");
                Console.WriteLine("3. Создать вариант ответа");
                Console.WriteLine("4. Изменить викторину");
                Console.WriteLine("5. Изменить вопрос");
                Console.WriteLine("6. Изменить вариант ответа");
                Console.WriteLine("7. Выйти");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateQuiz();
                        break;
                    case "2":
                        CreateQuestion();
                        break;
                    case "3":
                        CreateOption();
                        break;
                    case "4":
                        EditQuiz();
                        break;
                    case "5":
                        EditQuestion();
                        break;
                    case "6":
                        EditOption();
                        break;
                    case "7":
                        ShowMenu(User);
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        Console.ReadLine();
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
        private void Login()
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

                    User = UserManager.Login(login, password);
                    Console.WriteLine("Вы успешно вошли в систему. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.");
                }
            }
        }
        //Опции после входа в систему
        private void StartQuiz(User user)
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Выберите викторину за её номером: ");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    int choice = int.Parse(Console.ReadLine()) - 1;
                    if (choice < 1 || choice > quizzes.Count)
                        throw new Exception("Неверный номер викторины.");
                    QuizManager.StartQuiz(user, QuizManager.GetQuizzesByTitle(quizzes[choice]));
                    Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.");
                }
            }
        }
        private void ShowTop10Results(User user)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Выберите викторину за её номером: ");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    int choice = int.Parse(Console.ReadLine()) - 1;

                    if (choice < 1 || choice > quizzes.Count)
                        throw new Exception("Неверный номер викторины.");
                    var quiz = QuizManager.GetQuizzesByTitle(quizzes[choice]);
                    var results = ResultManager.GetTop10Results(user.Login);
                    Console.Clear();
                    Console.WriteLine($"Топ 10 результатов по викторине {quiz.Title}:");
                    foreach (var result in results)
                    {
                        Console.WriteLine(result);
                    }
                    Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.");
                }
            }
        }
        private void ShowHistory(User user)
        {
            Console.Clear();
            var results = ResultManager.GetResults(user.Login);
            Console.WriteLine($"История:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
        private void ChangeUserData(User user)
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите новый логин: ");
                    string login = Console.ReadLine();
                    if (login == null)
                        throw new Exception("Логин не может быть пустым.");

                    Console.WriteLine("Введите новый пароль: ");
                    string password = Console.ReadLine();
                    if (password == null)
                        throw new Exception("Пароль не может быть пустым.");

                    Console.WriteLine("Введите новый день своего рождения: ");
                    int day = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите новый месяц своего рождения: ");
                    int month = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите новый год своего рождения: ");
                    int year = int.Parse(Console.ReadLine());

                    user.Login = login;
                    user.Password = password;
                    user.DateOfBirth = new DateTime(year, month, day);
                    Console.WriteLine("Данные успешно изменены. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.");
                }
            }
        }
        //Опции для создания и изменения викторин
        private void CreateQuiz()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    QuizManager.CreateQuiz(title, new List<Question>());
                    Console.WriteLine("Викторина успешно создана. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private void CreateQuestion()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    Console.WriteLine("Введите текст вопроса: ");
                    string text = Console.ReadLine();
                    if (text == null)
                        throw new Exception("Текст вопроса не может быть пустым.");

                    QuizManager.CreateQuestion(QuizManager.GetQuizzesByTitle(title), text, new List<Option>());
                    Console.WriteLine("Вопрос успешно создан. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private void CreateOption()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    Console.WriteLine("Введите текст вопроса: ");
                    string question = Console.ReadLine();
                    if (question == null)
                        throw new Exception("Текст вопроса не может быть пустым.");

                    Console.WriteLine("Введите текст варианта ответа: ");
                    string text = Console.ReadLine();
                    if (text == null)
                        throw new Exception("Текст варианта ответа не может быть пустым.");

                    Console.WriteLine("Является ли вариант ответа правильным? (true/false): ");
                    bool isCorrect = bool.Parse(Console.ReadLine());

                    QuizManager.CreateOption(QuizManager.GetQuizzesByTitle(title), question, text, isCorrect);
                    Console.WriteLine("Вариант ответа успешно создан. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private void EditQuiz()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    Console.WriteLine("Введите новое название викторины: ");
                    string newTitle = Console.ReadLine();
                    if (newTitle == null)
                        throw new Exception("Название не может быть пустым.");

                    QuizManager.GetQuizzesByTitle(title).Title = newTitle;
                    Console.WriteLine("Название успешно изменено. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private void EditQuestion()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    Console.WriteLine("Введите текст вопроса: ");
                    string text = Console.ReadLine();
                    if (text == null)
                        throw new Exception("Текст вопроса не может быть пустым.");

                    Console.WriteLine("Введите новый текст вопроса: ");
                    string newText = Console.ReadLine();
                    if (newText == null)
                        throw new Exception("Текст вопроса не может быть пустым.");

                    QuizManager.GetQuizzesByTitle(title).Questions.FirstOrDefault(question => question.Text == text).Text = newText;
                    Console.WriteLine("Текст вопроса успешно изменен. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        private void EditOption()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите название викторины: ");
                    string title = Console.ReadLine();
                    if (title == null)
                        throw new Exception("Название не может быть пустым.");

                    Console.WriteLine("Введите текст вопроса: ");
                    string questionText = Console.ReadLine();
                    if (questionText == null)
                        throw new Exception("Текст вопроса не может быть пустым.");

                    Console.WriteLine("Введите текст варианта ответа: ");
                    string? text = Console.ReadLine();
                    if (text == null)
                        throw new Exception("Текст варианта ответа не может быть пустым.");

                    Console.WriteLine("Введите новый текст варианта ответа: ");
                    string newText = Console.ReadLine();
                    if (newText == null)
                        throw new Exception("Текст варианта ответа не может быть пустым.");

                    QuizManager.GetQuizzesByTitle(title).Questions.FirstOrDefault(question => question.Text == questionText).Options.FirstOrDefault(option => option.Text == text).Text = newText;
                    Console.WriteLine("Текст варианта ответа успешно изменен. Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }

}
