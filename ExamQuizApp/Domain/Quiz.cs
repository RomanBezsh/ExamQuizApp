﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.Domain
{
    public class Quiz
    {
        public string? Title { get; set; }
        public List<Question> Questions { get; set; }
    }
}
