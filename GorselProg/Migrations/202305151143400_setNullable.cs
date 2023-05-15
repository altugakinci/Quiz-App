namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Room", "CurrentGameId", c => c.Guid());
            AlterColumn("dbo.Room", "AdminId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Room", "AdminId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Room", "CurrentGameId", c => c.Guid(nullable: false));
        }
    }
}
