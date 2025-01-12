using ExamQuizApp.Domain;
using ExamQuizApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.UI
{
    internal class UI
    {
        public User User { get; set; }
        //Меню
        public void Start()
        {
            LoadData();
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
                        Console.WriteLine("Нажмите Enter, чтобы продолжить.");
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


                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        StartQuiz(User);
                        break;
                    case 2:
                        ShowHistory(User);
                        break;
                    case 3:
                        ShowTop10Results(User);
                        break;
                    case 4:
                        ChangeUserData(User);
                        break;
                    case 5:
                        ShowQuizEditor();
                        break;
                    case 6:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        Console.WriteLine("Нажмите Enter, чтобы продолжить.");
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
                Console.WriteLine("2. Изменить викторину");
                Console.WriteLine("3. Добавить вопросы к существующей викторине");
                Console.WriteLine("4. Добавить варианты к существующему вопросу");
                Console.WriteLine("5. Выйти");

                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CreateQuizWithQuestions();
                        break;
                    case 2:
                        EditQuiz();
                        break;
                    case 3:
                        AddQuestionsToExistingQuiz();
                        break;
                    case 4:
                        AddQuestionsToExistingQuiz();
                        break;
                    case 5:
                        Console.Clear();
                        return;
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
                    Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();

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
                    Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Попробуйте снова.");
                    Console.ReadLine();
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
                    Console.WriteLine("Выберите викторину за её номером (или напишите 0 для выхода): ");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 0)
                    {
                        return;
                    }
                    if (choice < 1 || choice > quizzes.Count)
                    {
                        throw new Exception("Неверный номер викторины.");
                    }
                    QuizManager.StartQuiz(user, QuizManager.GetQuizzesByTitle(quizzes[choice-1]));
                    Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
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
                    Console.WriteLine("Выберите викторину за её номером (или напишите 0 для выхода): ");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 0)
                    {
                        return;
                    }
                    if (choice < 1 || choice > quizzes.Count)
                        throw new Exception("Неверный номер викторины.");
                    var quiz = QuizManager.GetQuizzesByTitle(quizzes[choice-1]);
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
                    Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        private void ShowHistory(User user)
        {
            Console.Clear();
            var results = ResultManager.GetHistory(user.Login);
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
                    Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        //Опции для создания и изменения викторин
        private void CreateQuizWithQuestions()
        {
            Console.Clear();
            Console.WriteLine("Введите название викторины:");
            string quizTitle = Console.ReadLine();

            var questions = new List<Question>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите текст вопроса (или оставьте пустым для завершения):");
                string questionText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(questionText))
                {
                    break;
                }
                var options = new List<Option>();
                while (true)
                {
                    Console.WriteLine("Введите текст варианта ответа (или оставьте пустым для завершения):");
                    string optionText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(optionText))
                    {
                        break;
                    }
                    Console.WriteLine("Этот вариант правильный? (true/false):");
                    bool isCorrect = Boolean.Parse(Console.ReadLine());

                    options.Add(new Option
                    {
                        Text = optionText,
                        IsCorrect = isCorrect
                    });
                }

                questions.Add(new Question
                {
                    Text = questionText,
                    Options = options
                });
            }

            QuizManager.CreateQuiz(quizTitle, questions);
            Console.WriteLine("Викторина успешно создана. Нажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
        private void EditQuiz()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Выберите викторину для редактирования за её номером (или напишите 0 для выхода):");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 0)
                    {
                        return;
                    }
                    if (choice < 1 || choice > quizzes.Count)
                    {
                        throw new Exception("Неверный номер викторины.");
                    }
                    else if (choice == 0)
                    { 
                       return;
                    }
                    var quiz = QuizManager.GetQuizzesByTitle(quizzes[choice - 1]);
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Редактирование викторины: {quiz.Title}");
                        Console.WriteLine("1. Изменить название викторины");
                        Console.WriteLine("2. Изменить вопросы");
                        Console.WriteLine("3. Выйти");

                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                Console.WriteLine("Введите новое название викторины:");
                                quiz.Title = Console.ReadLine();
                                FileManager.SaveQuizzes(QuizManager.GetQuizzes());
                                break;
                            case "2":
                                EditQuestions(quiz);
                                break;
                            case "3":
                                return;
                            default:
                                Console.WriteLine("Неверный ввод. Попробуйте снова.");
                                Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                                Console.ReadLine();
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        private void EditQuestions(Quiz quiz)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Редактирование вопросов викторины: {quiz.Title}");
                    int index = 1;
                    foreach (var q in quiz.Questions)
                    {
                        Console.WriteLine($"{index++}. {q.Text}");
                    }
                    Console.WriteLine("Введите номер вопроса для редактирования (или оставьте пустым для завершения):");
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == default)
                    {
                        return;
                    }
                    if (choice < 1 || choice > quiz.Questions.Count)
                    {
                        throw new Exception("Неверный номер вопроса.");
                    }
                    var question = quiz.Questions[choice - 1];
                    Console.WriteLine("Введите новый текст вопроса:");
                    question.Text = Console.ReadLine();

                    EditOptions(question);
                    FileManager.SaveQuizzes(QuizManager.GetQuizzes());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        private void EditOptions(Question question)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Редактирование вариантов ответа для вопроса: {question.Text}");
                    int index = 1;
                    foreach (var op in question.Options)
                    {
                        Console.WriteLine($"{index++}. {op.Text} (Правильный: {op.IsCorrect})");
                    }
                    Console.WriteLine("Введите номер варианта для редактирования (или оставьте пустым для завершения):");
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == default)
                    {
                        return;
                    }
                    if (choice < 1 || choice > question.Options.Count)
                    {
                        throw new Exception("Неверный номер вопроса.");
                    }
                    var option = question.Options[choice - 1];
                    Console.WriteLine("Введите новый текст варианта:");
                    option.Text = Console.ReadLine();

                    Console.WriteLine("Этот вариант правильный? (true/false):");
                    option.IsCorrect = Boolean.Parse(Console.ReadLine());
                    FileManager.SaveQuizzes(QuizManager.GetQuizzes());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        private void AddQuestionsToExistingQuiz()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Выберите викторину для добавления вопросов за её номером:");
                    var quizzes = QuizManager.GetTitleOfQuizzes();
                    int index = 1;
                    foreach (var titleOfQuiz in quizzes)
                    {
                        Console.WriteLine($"{index++}. {titleOfQuiz}");
                    }
                    var choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == default)
                    {
                        return;
                    }
                    if (choice < 1 || choice > QuizManager.GetQuizzes().Count)
                    {
                        throw new Exception("Неверный номер викторины.");
                    }
                    var quiz = QuizManager.GetQuizzesByTitle(quizzes[choice - 1]);

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите текст вопроса (или оставьте пустым для завершения):");
                        string questionText = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(questionText))
                        {
                            break;
                        }
                        var options = new List<Option>();
                        while (true)
                        {
                            Console.WriteLine("Введите текст варианта ответа (или оставьте пустым для завершения):");
                            string optionText = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(optionText))
                            {
                                break;
                            }
                            Console.WriteLine("Этот вариант правильный? (true/false):");
                            bool isCorrect = Boolean.Parse(Console.ReadLine());
                            options.Add(new Option
                            {
                                Text = optionText,
                                IsCorrect = isCorrect
                            });
                        }
                        quiz.Questions.Add(new Question
                        {
                            Text = questionText,
                            Options = options
                        });
                    }
                    FileManager.SaveQuizzes(QuizManager.GetQuizzes());
                    Console.WriteLine("Вопросы успешно добавлены. Нажмите Enter, чтобы вернуться в меню.");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                    Console.WriteLine("Нажмите Enter, чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
        //Загрузка данных
        private void LoadData()
        {
            LoadStandartQuizzes();
            UserManager.LoadUsers();
            QuizManager.LoadQuizzes();
            ResultManager.LoadResults();
        }

        //Стандартные викторины
        private void LoadStandartQuizzes()
        {
            QuizManager.CreateQuiz("История", new List<Question>
            {
                new Question
                {
                    Text = "Кто был первым президентом Соединенных Штатов Америки?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Кто был первым президентом Соединенных Штатов Америки?", IsCorrect = false },
                        new Option { Text = "Джордж Вашингтон", IsCorrect = true },
                        new Option { Text = "Авраам Линкольн", IsCorrect = false },
                        new Option { Text = "Джон Адамс", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Какое событие стало началом Второй мировой войны?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Нападение на Перл-Харбор", IsCorrect = false },
                        new Option { Text = "Нападение на Польшу", IsCorrect = true },
                        new Option { Text = "Битва при Сталинграде", IsCorrect = false },
                        new Option { Text = "Высадка в Нормандии", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Какая империя была известна своей системой дорог и акведуков?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Греческая империя", IsCorrect = false },
                        new Option { Text = "Римская империя", IsCorrect = true },
                        new Option { Text = "Османская империя", IsCorrect = false },
                        new Option { Text = "Персидская империя", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Какая страна первой отправила человека в космос?\r\n\r\n",
                    Options = new List<Option>
                    {
                        new Option { Text = "Соединенные Штаты Америки", IsCorrect = false },
                        new Option { Text = "Китай", IsCorrect = false },
                        new Option { Text = "Советский Союз", IsCorrect = true },
                        new Option { Text = "Франция", IsCorrect = false }
                    }
                }
            });
            QuizManager.CreateQuiz("Биология", new List<Question>
            {
                new Question
                {
                    Text = "Какая органелла отвечает за синтез белков?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Лизосома", IsCorrect = false },
                        new Option { Text = "Рибосома", IsCorrect = true },
                        new Option { Text = "Митохондрия", IsCorrect = false },
                        new Option { Text = "Ядро", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Какое основание входит в состав ДНК, но отсутствует в РНК?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Аденин", IsCorrect = false },
                        new Option { Text = "Урацил", IsCorrect = false },
                        new Option { Text = "Тимин", IsCorrect = true },
                        new Option { Text = "Цитозин", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Где в растительной клетке происходит фотосинтез?",
                    Options = new List<Option>
                    {
                        new Option { Text = "В вакуоли", IsCorrect = false },
                        new Option { Text = "В ядре", IsCorrect = false },
                        new Option { Text = "В хлоропластах", IsCorrect = true },
                        new Option { Text = "В клеточной стенке", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Кто является автором теории естественного отбора?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Жан-Батист Ламарк", IsCorrect = false },
                        new Option { Text = "Чарльз Дарвин", IsCorrect = true },
                        new Option { Text = "Альфред Уоллес", IsCorrect = false },
                        new Option { Text = "Карл Линней", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "К какому царству относятся дрожжи?",
                    Options = new List<Option>
                    {
                        new Option { Text = "Бактерии", IsCorrect = false },
                        new Option { Text = "Грибы", IsCorrect = true },
                        new Option { Text = "Растения", IsCorrect = false },
                        new Option { Text = "Животные", IsCorrect = false }
                    }
                }
            });

        }
    }

}
