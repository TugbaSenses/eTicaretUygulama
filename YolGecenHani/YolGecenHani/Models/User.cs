using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public double MoneyPoint { get; set; } = 50;
        public double Money{ get; set; } = 0;
        public string Gender { get; set; }
        public string Phone { get; set; }

      
        public DateTime BirthDay { get; set; } = DateTime.Parse("1900 - 01 - 01 00:00:00");
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public int ErrorPasswordEntry { get; set; } = 0;
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; } = 1;
        public Role Role { get; set; }
        public int BadgeId { get; set; } = 1;
        public Badge Badge { get; set; }
        public int BadgePoint { get; set; } = 0;
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        public List<Restaurant> Restaurant { get; set; }
        public List<Order> Order { get; set; }
        public List<RestaurantComment> RestaurantComment { get; set; }
        public List<UserAddress> UserAddress { get; set; }
    }
}