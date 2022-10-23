using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        public List<Product> Product { get; set; }
    }
}