using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YolGecenHani.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserAddressId { get; set; }
        public UserAddress UserAddress { get; set; }
        public int OrderStatusId { get; set; } = 1;
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool OrderCommentStatus { get; set; }
        public List<OrderProduct> OrderProduct { get; set; }
        public List<Invoice> Invoice { get; set; }
    }
}