using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        public List<OrderProduct> OrderProduct { get; set; }
    }
}