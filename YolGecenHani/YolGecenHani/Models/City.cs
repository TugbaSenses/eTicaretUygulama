using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        public List<Restaurant> Restaurant { get; set; }
        public List<UserAddress> UserAddress { get; set; }

    }
}