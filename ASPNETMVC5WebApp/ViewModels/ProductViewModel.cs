using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.ViewModels
{
    public class ProductViewModel
    {
        // Product Category View Model
        public Product Product { get; set; } // from Product Model
        public IEnumerable<Category> Categories { get; set; } // list of Category for Product
    }
}