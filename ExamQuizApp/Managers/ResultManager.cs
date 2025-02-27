﻿using System;
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
            FileManager.SaveResults(_results);
        }
        public static void LoadResults()
        {
            _results = FileManager.LoadResults();
        }
        public static List<string> GetHistory(string login)
        {
            return _results.Where(r => r.UserLogin == login).Select(r => $"{r.TitleOfQuiz} {r.Score}").ToList();
        }
        public static List<string> GetTop10Results(string login, string titleOfQuiz)
        {
            return _results.Where(r => r.UserLogin == login && r.TitleOfQuiz == titleOfQuiz).OrderByDescending(r => r.Score).Select(r => r.TitleOfQuiz + " " + r.Score).ToList();
        }
    }
}
