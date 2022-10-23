using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.HomeModel
{
    public class OrderShippingAddress
    {
        public string OrderNumber { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }
    }
}