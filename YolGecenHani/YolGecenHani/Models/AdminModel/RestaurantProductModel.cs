using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.AdminModel
{
    public class RestaurantProductModel
    {
        public Product Product { get; set; }
        public List<Product> ProductList { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Discount> DiscountList { get; set; }
    }
}