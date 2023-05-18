namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AnswerText = c.String(nullable: false),
                        UserId = c.Guid(),
                        QuestionId = c.Guid(),
                        GameId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        RoomId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionText = c.String(nullable: false),
                        OptionsText = c.String(nullable: false),
                        CorrectAnswerIndex = c.Int(nullable: false),
                        CategoryId = c.Guid(),
                        GameId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Game", t => t.GameId)
                .Index(t => t.CategoryId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 255),
                        CurrentGameId = c.Guid(),
                        AdminId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AdminId)
                .ForeignKey("dbo.Game", t => t.CurrentGameId)
                .Index(t => t.CurrentGameId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.BannedUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        RoomId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(maxLength: 255),
                        Password = c.String(nullable: false),
                        Salt = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                        Xp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessageText = c.String(nullable: false, maxLength: 255),
                        SentTime = c.DateTime(nullable: false),
                        UserId = c.Guid(),
                        RoomId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomId = c.Guid(),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Room", "CurrentGameId", "dbo.Game");
            DropForeignKey("dbo.Game", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Room", "AdminId", "dbo.User");
            DropForeignKey("dbo.Players", "UserId", "dbo.User");
            DropForeignKey("dbo.Players", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "RoomId", "dbo.Room");
            DropForeignKey("dbo.BannedUsers", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropForeignKey("dbo.BannedUsers", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Question", "GameId", "dbo.Game");
            DropForeignKey("dbo.Question", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "GameId", "dbo.Game");
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.Players", new[] { "RoomId" });
            DropIndex("dbo.Message", new[] { "RoomId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.BannedUsers", new[] { "RoomId" });
            DropIndex("dbo.BannedUsers", new[] { "UserId" });
            DropIndex("dbo.Room", new[] { "AdminId" });
            DropIndex("dbo.Room", new[] { "CurrentGameId" });
            DropIndex("dbo.Question", new[] { "GameId" });
            DropIndex("dbo.Question", new[] { "CategoryId" });
            DropIndex("dbo.Game", new[] { "RoomId" });
            DropIndex("dbo.Answer", new[] { "GameId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropTable("dbo.Players");
            DropTable("dbo.Message");
            DropTable("dbo.User");
            DropTable("dbo.BannedUsers");
            DropTable("dbo.Room");
            DropTable("dbo.Category");
            DropTable("dbo.Question");
            DropTable("dbo.Game");
            DropTable("dbo.Answer");
        }
    }
}
