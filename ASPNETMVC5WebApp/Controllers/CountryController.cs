using ASPNETMVC5WebApp.Data;
using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            // build country list here
            List<Country> countries = new List<Country>();
            CountriesRepository c = new CountriesRepository();
            countries.AddRange(c.GetAllCountry());
            //ViewBag.Countries = countries;
            return View(countries);
        }

        
    }
}