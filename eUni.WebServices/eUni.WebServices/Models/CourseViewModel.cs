﻿using System;

namespace eUni.WebServices.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }   
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}