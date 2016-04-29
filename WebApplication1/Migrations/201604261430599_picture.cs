namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "pictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "pictureUrl");
        }
    }
}
