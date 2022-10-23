using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Direction { get; set; }
        public string OpenAddress { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public bool Selected { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public List<Order> Order { get; set; }
    }
}