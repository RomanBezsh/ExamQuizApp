﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamQuizApp.Domain
{
    public class Question
    {
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }
}
