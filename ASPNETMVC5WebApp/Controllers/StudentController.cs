using ASPNETMVC5WebApp.Models;
using ASPNETMVC5WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class StudentController : Controller
    {
        //// GET: Student
        //public ActionResult Index()
        //{

        //    var students = new List<Student>()
        //    {
        //        new Student()
        //        {
        //            Id=1,
        //            StudentName = "Student A",
        //            Address = "A Address"
        //        },
        //        new Student()
        //        {
        //            Id=1,
        //            StudentName = "Student B",
        //            Address = "B Address"
        //        },
        //        new Student()
        //        {
        //            Id=3,
        //            StudentName = "Student C",
        //            Address = "C Address"
        //        },
        //        new Student()
        //        {
        //            Id=4,
        //            StudentName = "Student D",
        //            Address = "D Address"
        //        },

        //    };

        //    return View(students);
        //}


        // GET: Student
        public ActionResult GetStudents()
        {

            var student = new Student()
            {
                Id = 1,
                StudentName = "Student A",
                Address = "Student Address A"
            };
            var subjects = new List<Subject>()
            {
                new Subject() { Id = 1, SubjectName ="Subject AAA" },
                new Subject() { Id = 2, SubjectName ="Subject BBB" },
                new Subject() { Id = 3, SubjectName ="Subject CCC" }
            };

            // create view model here
            var viewModel = new StudentViewModel()
            {
                Student = student,
                Subjects = subjects
            };

            return View(viewModel); // View model data pass to View
        }

        
        public ActionResult GetStudents123()
        {
            var student = new Student() { Id = 1, StudentName = "ABCD", Address = "ABCD" };
            return View(student); //single model data pass to View
        }

        public ActionResult EditStudent(int id)
        {
            return Content("Student Id >>> " + id);
        }

        // Attribute Routing
        // Range, Min, Max, and Regex for Regular expressions
        // datatype
        [Route("student/bypassingyear/{mth:int:range(1,12)}/{yr:int:min(1900)}")]
        public ActionResult ByPassingYear(int mth, int yr)
        {
            return Content("Student >>> month : " + mth + ", year : " + yr);
        }



        //
    }
}