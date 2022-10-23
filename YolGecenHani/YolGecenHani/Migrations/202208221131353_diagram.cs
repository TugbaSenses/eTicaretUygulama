namespace YolGecenHani.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diagram : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "RoleId");
            CreateIndex("dbo.Users", "BadgeId");
            CreateIndex("dbo.Orders", "UserId");
            CreateIndex("dbo.Orders", "UserAddressId");
            CreateIndex("dbo.Orders", "OrderStatusId");
            CreateIndex("dbo.Invoices", "OrderId");
            CreateIndex("dbo.OrderProducts", "OrderId");
            CreateIndex("dbo.OrderProducts", "ProductId");
            CreateIndex("dbo.Products", "CategoryId");
            CreateIndex("dbo.Products", "DiscountId");
            CreateIndex("dbo.Categories", "RestaurantId");
            CreateIndex("dbo.Restaurants", "CityId");
            CreateIndex("dbo.Restaurants", "UserId");
            CreateIndex("dbo.UserAddresses", "UserId");
            CreateIndex("dbo.UserAddresses", "CityId");
            CreateIndex("dbo.RestaurantComments", "RestaurantId");
            CreateIndex("dbo.RestaurantComments", "UserId");
            AddForeignKey("dbo.Users", "BadgeId", "dbo.Badges", "Id");
            AddForeignKey("dbo.Invoices", "OrderId", "dbo.Orders", "Id");
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "Id");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Categories", "RestaurantId", "dbo.Restaurants", "Id");
            AddForeignKey("dbo.Restaurants", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.UserAddresses", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Orders", "UserAddressId", "dbo.UserAddresses", "Id");
            AddForeignKey("dbo.UserAddresses", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.RestaurantComments", "RestaurantId", "dbo.Restaurants", "Id");
            AddForeignKey("dbo.RestaurantComments", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Restaurants", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Products", "DiscountId", "dbo.Discounts", "Id");
            AddForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus", "Id");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "DiscountId", "dbo.Discounts");
            DropForeignKey("dbo.Restaurants", "UserId", "dbo.Users");
            DropForeignKey("dbo.RestaurantComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.RestaurantComments", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserAddressId", "dbo.UserAddresses");
            DropForeignKey("dbo.UserAddresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Restaurants", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Categories", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Invoices", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Users", "BadgeId", "dbo.Badges");
            DropIndex("dbo.RestaurantComments", new[] { "UserId" });
            DropIndex("dbo.RestaurantComments", new[] { "RestaurantId" });
            DropIndex("dbo.UserAddresses", new[] { "CityId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.Restaurants", new[] { "UserId" });
            DropIndex("dbo.Restaurants", new[] { "CityId" });
            DropIndex("dbo.Categories", new[] { "RestaurantId" });
            DropIndex("dbo.Products", new[] { "DiscountId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.Invoices", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropIndex("dbo.Orders", new[] { "UserAddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "BadgeId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
        }
    }
}
