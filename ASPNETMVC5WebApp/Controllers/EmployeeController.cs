using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.VIndexName = "Employee Index View";

            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeID = 1,
                    EmployeeName = "Employee Name ABC",
                    Address = "Your Address 0",
                    DateofJoining = System.DateTime.Now,
                    MaritalStatus = 1,
                    IsEligibleForloan = true,
                    Salary = 0.00m,
                    CreatedBy = "ADMIN",
                    CreatedDate = System.DateTime.Now,

                    UpdatedBy = "ADMIN",
                    UpdatedDate = System.DateTime.Now,
                    RecordStatus = 0
                },
                new Employee()
                {
                    EmployeeID = 2,
                    EmployeeName = "Employee Name DEF",
                    Address = "Your Address 1",
                    DateofJoining = System.DateTime.Now,
                    MaritalStatus = 0,
                    IsEligibleForloan = true,
                    Salary = 0.00m,
                    CreatedBy = "ADMIN",
                    CreatedDate = System.DateTime.Now,

                    UpdatedBy = "ADMIN",
                    UpdatedDate = System.DateTime.Now,
                    RecordStatus = 0
                },
                new Employee()
                {
                    EmployeeID = 3,
                    EmployeeName = "Employee Name GHI",
                    Address = "Your Address 3",
                    DateofJoining = System.DateTime.Now,
                    MaritalStatus = 1,
                    IsEligibleForloan = true,
                    Salary = 0.00m,
                    CreatedBy = "ADMIN",
                    CreatedDate = System.DateTime.Now,

                    UpdatedBy = "ADMIN",
                    UpdatedDate = System.DateTime.Now,
                    RecordStatus = 0
                }
            };
            
            // ViewBag can pass dymanic property "Employee" to veiw
            ViewBag.Employees = employees;

            return View();
        }
        

    }
}