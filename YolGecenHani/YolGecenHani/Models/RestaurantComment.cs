using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class RestaurantComment
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
    }
}