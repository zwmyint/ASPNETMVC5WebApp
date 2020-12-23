using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // passing data to veiw from controller // ViewBag can pass dymanic property data to veiw
            ViewBag.FirstName = "My First Name";
            ViewBag.City = "Singapore";
            ViewBag.NNumber = 99;

            // another way of passing data from controller to view
            TempData["Employee"] = "Employee Name here";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}