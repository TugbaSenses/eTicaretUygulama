using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace YolGecenHani.Models.EntityModel
{
    public class DataContext:DbContext
    {
        public DataContext():base("YolGecenHaniDBConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();//çoka çok diagram ilişkileri
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//bire çok diagram ilişkileri
        }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Badge> Badge { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<RestaurantComment> RestaurantComment { get; set; }
    }
}