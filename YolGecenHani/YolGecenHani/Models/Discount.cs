using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public double DiscountPrice { get; set; } = 0;
        public double DiscountRate { get; set; } = 0;
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public List<Product> Product { get; set; }
    }
}