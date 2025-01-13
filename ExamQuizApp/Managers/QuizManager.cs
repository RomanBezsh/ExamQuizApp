using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamQuizApp.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace ExamQuizApp.Managers
{
    public static class QuizManager
    {
        private static List<Quiz> _quizzes = new List<Quiz>();
        public static void LoadQuizzes()
        {
            _quizzes = FileManager.LoadQuizzes();
        }
        public static List<Quiz> GetQuizzes()
        {
            return _quizzes;
        }
        public static List<string> GetTitleOfQuizzes() 
        {
            return _quizzes.Select(quiz => quiz.Title).ToList();
        }
        public static Quiz GetQuizzesByTitle(string title)
        {

           return _quizzes.FirstOrDefault(quiz => quiz.Title == title);
        }
        public static void StartQuiz(User user, Quiz quiz)
        {
            Console.Clear();
            Console.WriteLine($"Начинаем викторину: {quiz.Title}");
            int score = 0;
            foreach (var question in quiz.Questions)
            {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i].Text}");
                }
                int userAnswer;
                while (!int.TryParse(Console.ReadLine(), out userAnswer) || userAnswer < 1 || userAnswer > question.Options.Count)
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите номер одного из вариантов.");
                }
                if (question.Options[userAnswer - 1].IsCorrect)
                {
                    score++;
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Викторина завершена! Ваш результат: {score}/{quiz.Questions.Count}");
            ResultManager.AddResult(new Result
            {
                UserLogin = user.Login,
                TitleOfQuiz = quiz.Title,
                Score = score
            });
        }
        public static void CreateQuiz(string title, List<Question> questions)
        {
            if (_quizzes.Any(quiz => quiz.Title == title))
            {
                throw new Exception("Такая викторина уже существует");
            }
            _quizzes.Add(new Quiz
            {
                Title = title,
                Questions = questions
            });
            FileManager.SaveQuizzes(_quizzes);
        }
        public static void CreateQuestion(Quiz quiz, string text, List<Option> options)
        {
            if (quiz.Questions.Any(question => question.Text == text))
            {
                throw new Exception("Такой вопрос уже существует");
            }
            quiz.Questions.Add(new Question
            {
                Text = text,
                Options = options
            });
            FileManager.SaveQuizzes(_quizzes);
        }
        public static void CreateOption(Quiz quiz, string question, string text, bool isCorrect)
        {
            var targetQuestion = quiz.Questions.FirstOrDefault(q => q.Text == question);
            if (targetQuestion == null)
            {
                throw new Exception("Вопрос не найден");
            }
            if (targetQuestion.Options.Any(option => option.Text == text))
            {
                throw new Exception("Такой вариант уже существует");
            }
            targetQuestion.Options.Add(new Option
            {
                Text = text,
                IsCorrect = isCorrect
            });
            FileManager.SaveQuizzes(_quizzes);
        }
        public static Result CreateResult(string userLogin, string titleOfQuiz, int score)
        {
            return new Result
            {
                UserLogin = userLogin,
                TitleOfQuiz = titleOfQuiz,
                Score = score
            };
        }
        public static void DeleteQuiz(string title)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Title == title);
            if (quiz == null)
            {
                throw new Exception("Викторина не найдена");
            }
            _quizzes.Remove(quiz);
            FileManager.SaveQuizzes(_quizzes);
        }
    }
}
