using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.ViewModels
{
    public class StudentViewModel
    {
        // Student Subject View Model
        public Student Student { get; set; } // from Student Model
        public IEnumerable<Subject> Subjects { get; set; } // list of Subject for Student
    }
}