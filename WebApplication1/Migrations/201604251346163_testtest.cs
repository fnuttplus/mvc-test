namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "counter");
        }
    }
}
