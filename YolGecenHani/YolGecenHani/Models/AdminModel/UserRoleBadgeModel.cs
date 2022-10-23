using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models.AdminModel
{
    public class UserRoleBadgeModel
    {
        public User User { get; set; }
        public List<User> UserList { get; set; }
        public List<Role> Role { get; set; }
        public List<Badge> Badge { get; set; }
    }
}