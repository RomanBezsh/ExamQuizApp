using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.Domain
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
