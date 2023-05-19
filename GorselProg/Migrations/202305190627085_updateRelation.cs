namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Question", "GameId", "dbo.Game");
            DropIndex("dbo.Question", new[] { "GameId" });
            CreateTable(
                "dbo.GameQuestion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GameId = c.Guid(),
                        QuestionId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.GameId)
                .Index(t => t.QuestionId);
            
            DropColumn("dbo.Question", "GameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "GameId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.GameQuestion", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.GameQuestion", "GameId", "dbo.Game");
            DropIndex("dbo.GameQuestion", new[] { "QuestionId" });
            DropIndex("dbo.GameQuestion", new[] { "GameId" });
            DropTable("dbo.GameQuestion");
            CreateIndex("dbo.Question", "GameId");
            AddForeignKey("dbo.Question", "GameId", "dbo.Game", "Id");
        }
    }
}
