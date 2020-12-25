using ASPNETMVC5WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            //
            var repo = new CustomersRepository();
            var customerList = repo.GetCustomers();
            return View(customerList);
        }

        [HttpGet]
        public ActionResult GetRegions(string Iso3)
        {
            if (!string.IsNullOrWhiteSpace(Iso3) && Iso3.Length == 3)
            {
                var repo = new RegionsRepository();

                IEnumerable<SelectListItem> regions = repo.GetRegions(Iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            // New Form for Create New Customer
            var repo = new CustomersRepository();
            var customer = repo.CreateCustomer();
            return View(customer);
        }


        //
    }
}