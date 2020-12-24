using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Models
{
    public class MySkills
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Skills { get; set; }
    }
}