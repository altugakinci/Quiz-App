namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gainedXp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answer", "GainedXp", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answer", "GainedXp");
        }
    }
}
