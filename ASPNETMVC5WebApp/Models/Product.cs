using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Product Name is Required.")]
        [Display(Name="Product Name")]
        [StringLength(150,ErrorMessage ="Product Name should be less than 150 characters long.")]
        public string ProductName{ get; set; }

        [Required(ErrorMessage = "Product Price is Required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Supplier Name is Required.")]
        [Display(Name = "Supplier Name")]
        public string Supplier { get; set; }
    }
}