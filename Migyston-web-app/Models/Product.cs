using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Migyston_web_app.Models
{
    public class Product
    {
        public int id { get; set; }

        [Required]
        public string? product_title { get; set; }
        public string? ean_number { get; set; }
        public string? product_description { get; set; }
        public int? amount_storage { get; set; }
        public decimal? price { get; set; }
        public string? expiration_date { get; set; }
        public ProductSize category { get; set; }
        public bool isSwedish { get; set; }

    }
        public enum ProductSize { Small, Medium, Large }
   
}