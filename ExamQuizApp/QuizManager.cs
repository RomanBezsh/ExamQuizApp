using ExamQuizApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp
{
    public static class QuizManager
    {
        private static List<Quiz> _quizzes = new List<Quiz>();
        public static void AddQuiz(Quiz quiz)
        {
            _quizzes.Add(quiz);
        }
        public static Quiz GetRandomQuiz()
        {
            var random = new Random();
            int index = random.Next(_quizzes.Count);
            return _quizzes[index];
        }
        public static List<Quiz> GetAllQuizzes()
        {
            return _quizzes;
        }

    }
}
