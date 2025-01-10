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
        public static List<string> GetTitleOfQuizzes() => _quizzes.Select(quiz => quiz.Title).ToList();
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
    }
}
