using ExamQuizApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.Managers
{
    public static class QuestionManager
    {
        public static void AddQuestion(Quiz quiz, string text, List<Option> options, string correctAnswers)
        {
            var question = new Question()
            {
                Text = text,
                Options = options
            };
            quiz.Questions.Add(question);
        }
    }
}
