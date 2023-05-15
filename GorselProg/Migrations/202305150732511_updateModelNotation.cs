namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelNotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Message", "MessageText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Message", "MessageText", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 255, fixedLength: true));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Code", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
