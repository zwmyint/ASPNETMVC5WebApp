﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName{ get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
    }
}