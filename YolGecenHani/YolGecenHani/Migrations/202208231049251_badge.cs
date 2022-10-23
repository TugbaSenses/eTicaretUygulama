namespace YolGecenHani.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Money", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "BadgePoint", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "BadgePoint");
            DropColumn("dbo.Users", "Money");
        }
    }
}
