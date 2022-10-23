using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Score { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public string BadgeColor { get; set; } = "Gray";
        public List<User> User { get; set; }

    }
}