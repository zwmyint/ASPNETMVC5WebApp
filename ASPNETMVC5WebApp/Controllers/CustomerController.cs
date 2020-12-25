using ASPNETMVC5WebApp.Data;
using ASPNETMVC5WebApp.ViewModels;
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


        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerID, CustomerName, SelectedCountryIso3, SelectedRegionCode")] CustomerEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var repo = new CustomersRepository();
                    bool saved = repo.SaveCustomer(model);
                    if (saved)
                    {
                        return RedirectToAction("Index");
                    }
                }
                // Handling model state errors is beyond the scope of the demo, 
                // so just throwing an ApplicationException when the ModelState is invalid
                // and rethrowing it in the catch block.
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }

        //
    }
}