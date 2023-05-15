namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room", "AdminId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "AdminId");
        }
    }
}
