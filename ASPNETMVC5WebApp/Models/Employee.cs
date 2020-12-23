using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Models
{
    public class Employee // this model represent entity from database
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public DateTime DateofJoining { get; set; }
        public int MaritalStatus { get; set; }
        public bool IsEligibleForloan { get; set; }
        public decimal Salary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int RecordStatus { get; set; }
    }
}