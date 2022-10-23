using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.AdminModel
{
    public class UserAddressCityModel
    {
        public User User { get; set; }
        public List<User> UserList { get; set; }
        public UserAddress UserAddress { get; set; }
        public List<UserAddress> UserAddressList { get; set; }
        public List<City> CityList { get; set; }
    }
}