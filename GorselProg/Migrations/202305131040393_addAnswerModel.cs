namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAnswerModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        Game_Id = c.Int(),
                        Question_Id = c.Int(),
                        Room_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Room_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Questions", "OptionsText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Answers", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Game_Id", "dbo.Games");
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "Room_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "Game_Id" });
            DropColumn("dbo.Questions", "OptionsText");
            DropTable("dbo.Answers");
        }
    }
}
