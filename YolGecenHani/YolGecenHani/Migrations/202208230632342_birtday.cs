namespace YolGecenHani.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birtday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "BirthDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
