namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initUserHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "salt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "salt");
        }
    }
}
