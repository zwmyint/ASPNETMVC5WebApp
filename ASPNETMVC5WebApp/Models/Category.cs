using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Models
{
    public class Category
    {
        [Display(Name = "Category ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is Required.")]
        [Display(Name = "Category Name")]
        [StringLength(150, ErrorMessage = "Category Name should be less than 50 characters long.")]
        public string CategoryName { get; set; }

        public int Status { get; set; }
    }
}