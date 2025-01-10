using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamQuizApp.Domain;


namespace ExamQuizApp.Managers
{
    public static class ResultManager
    {
        private static List<Result> _results = new List<Result>();

        public static void AddResult(Result result)
        {
            _results.Add(result);
        }
        public static List<string> GetResult(string login)
        { 
            var results = _results.Where(r => r.UserLogin == login).Select(r => r.TitleOfQuiz + " " + r.Score).ToList();
            return results;
        }
    }
}
