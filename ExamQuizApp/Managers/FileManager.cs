using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using ExamQuizApp.Domain;

namespace ExamQuizApp.Managers
{
    internal static class FileManager
    {
        private static readonly string UsersFilePath = "users.json";
        private static readonly string QuizzesFilePath = "quizzes.json";
        private static readonly string ResultsFilePath = "results.json";

        //public static void Save(List<User> users, List<Quiz> quizzes, List<Result> results)
        //{
        //    SaveToFile(UsersFilePath, users);
        //    SaveToFile(QuizzesFilePath, quizzes);
        //    SaveToFile(ResultsFilePath, results);
        //}
        public static void SaveUsers(List<User> users)
        {
            SaveToFile(UsersFilePath, users);
        }
        public static void SaveQuizzes(List<Quiz> quizzes)
        {
            SaveToFile(QuizzesFilePath, quizzes);
        }
        public static void SaveResults(List<Result> results)
        {
            SaveToFile(ResultsFilePath, results);
        }
        private static void SaveToFile<T>(string filePath, List<T> data)
        {
            var json = JsonSerializer.Serialize(data) ?? throw new JsonException("Serialize error");
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            using FileStream fstream = File.Create(filePath);
            fstream.Write(bytes, 0, bytes.Length);
        }
        public static List<User> LoadUsers()
        {
            return LoadFromFile<User>(UsersFilePath);
        }
        public static List<Quiz> LoadQuizzes()
        {
            return LoadFromFile<Quiz>(QuizzesFilePath);
        }
        public static List<Result> LoadResults()
        {
            return LoadFromFile<Result>(ResultsFilePath);
        }
        private static List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? throw new JsonException("Deserialize error");
        }
    }
}
