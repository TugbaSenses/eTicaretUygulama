using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        public List<Category> Category { get; set; }
        public List<RestaurantComment> RestaurantComment { get; set; }
    }
}