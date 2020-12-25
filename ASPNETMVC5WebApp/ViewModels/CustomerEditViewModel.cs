﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.ViewModels
{
    public class CustomerEditViewModel
    {
        [Display(Name = "Customer Number")]
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(75)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string SelectedCountryIso3 { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name = "State / Region")]
        public string SelectedRegionCode { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
    }
}