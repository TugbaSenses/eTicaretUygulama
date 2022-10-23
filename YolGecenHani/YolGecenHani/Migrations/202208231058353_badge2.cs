namespace YolGecenHani.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badge2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Badges", "BadgeColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Badges", "BadgeColor");
        }
    }
}
