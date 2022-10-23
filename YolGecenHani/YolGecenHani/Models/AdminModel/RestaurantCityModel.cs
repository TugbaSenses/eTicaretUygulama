using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YolGecenHani.Models;

namespace YolGecenHani.Models.AdminModel
{
    public class RestaurantCityModel
    {
        public Restaurant Restaurant { get; set; }
        public List<City> CityList { get; set; }
    }
}