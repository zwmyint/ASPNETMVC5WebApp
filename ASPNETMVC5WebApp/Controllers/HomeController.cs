using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class HomeController : Controller
    {
        public enum MySkills
        {
            ASPNETMVC,
            ASPNETWEPAPI,
            CSHARP,
            DOCUSIGN,
            JQUERY
        }

        public struct ConvertEnum
        {
            public int Value
            {
                get;
                set;
            }
            public String Text
            {
                get;
                set;
            }
        }

        public ActionResult Index()
        {
            // passing data to veiw from controller // ViewBag can pass dymanic property data to veiw
            ViewBag.FirstName = "My First Name";
            ViewBag.City = "Singapore";
            ViewBag.NNumber = 99;

            // another way of passing data from controller to view
            TempData["Employee"] = "Employee Name here";

            //
            List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
                  new SelectListItem {Text="Select",Value="0" },
                  new SelectListItem {Text="ASP.NET",Value="1" },
                  new SelectListItem {Text="C#",Value="2"},
                  new SelectListItem {Text="MVC",Value="3" },
                  new SelectListItem {Text="SQL",Value="4" },
            };
            ViewBag.ListItem = ObjItem;
            //

            //
            List<SelectListItem> mySkills = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "ASP.NET MVC", Value = "1"
                },
                new SelectListItem {
                    Text = "ASP.NET WEB API", Value = "2"
                },
                new SelectListItem {
                    Text = "ENTITY FRAMEWORK", Value = "3"
                },
                new SelectListItem {
                    Text = "DOCUSIGN", Value = "4"
                },
                new SelectListItem {
                    Text = "ORCHARD CMS", Value = "5"
                },
                new SelectListItem {
                    Text = "JQUERY", Value = "6"
                },
                new SelectListItem {
                    Text = "ZENDESK", Value = "7"
                },
                new SelectListItem {
                    Text = "LINQ", Value = "8"
                },
                new SelectListItem {
                    Text = "C#", Value = "9"
                },
                new SelectListItem {
                    Text = "GOOGLE ANALYTICS", Value = "10"
                },
            };
            ViewData["MySkills"] = mySkills;
            //

            //
            var myskill = new List<ConvertEnum>();
            foreach (MySkills lang in Enum.GetValues(typeof(MySkills)))
                myskill.Add(new ConvertEnum
                {
                    Value = (int)lang,
                    Text = lang.ToString()
                });
            ViewBag.MySkillEnum = myskill;


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