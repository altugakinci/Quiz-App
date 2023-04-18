namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userLevelXp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "level", c => c.Int(nullable: false,defaultValue:1));
            AddColumn("dbo.Users", "xp", c => c.Int(nullable: false,defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "xp");
            DropColumn("dbo.Users", "level");
        }
    }
}
