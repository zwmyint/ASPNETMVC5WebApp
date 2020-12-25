﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Models
{
    public class Country
    {
        [Key]
        [MaxLength(3)]
        public string Iso3 { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryNameEnglish { get; set; }

        public virtual IEnumerable<Region> Regions { get; set; }
    }
}