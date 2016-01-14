﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EUni_Client.Models
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}