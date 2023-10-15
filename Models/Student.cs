using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Exam8.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string StName { get; set; }
        public int Age { get; set; }
        public int Marks { get; set; }
        public DateTime DoB { get; set; }
        public bool IsActive { get; set; }
    }
}
