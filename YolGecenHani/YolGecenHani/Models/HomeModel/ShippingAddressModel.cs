using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.HomeModel
{
    public class ShippingAddressModel
    {
        public User User { get; set; }
        public List<City> City  { get; set; }
        public UserAddress UserAddress { get; set; }
    }
}