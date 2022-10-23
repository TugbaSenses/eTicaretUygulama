using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.HomeModel
{
    public class HomeProductModel
    {
        public List<Product> Product { get; set; }
        public List<Discount> Discount { get; set; }
        public List<Category> Category { get; set; }
    }
}