namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initModelDesing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AnswerText = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AnswerId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .Index(t => t.AnswerId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionText = c.String(nullable: false),
                        OptionsText = c.String(nullable: false),
                        CorrectAnswerIndex = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        AnswersId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Answers", t => t.AnswersId)
                .Index(t => t.CategoryId)
                .Index(t => t.AnswersId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        GameId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionsId = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionsId)
                .Index(t => t.QuestionsId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        GameId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.Game", t => t.GameId)
                .Index(t => t.RoomId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 255),
                        PlayersId = c.Guid(nullable: false),
                        GamesId = c.Guid(nullable: false),
                        MessagesId = c.Guid(nullable: false),
                        CurrentGameId = c.Guid(nullable: false),
                        BannedUsersId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BannedUsers", t => t.BannedUsersId)
                .ForeignKey("dbo.Messages", t => t.MessagesId)
                .ForeignKey("dbo.Players", t => t.PlayersId)
                .ForeignKey("dbo.Games", t => t.GamesId)
                .Index(t => t.PlayersId)
                .Index(t => t.GamesId)
                .Index(t => t.MessagesId)
                .Index(t => t.BannedUsersId);
            
            CreateTable(
                "dbo.BannedUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255, fixedLength: true),
                        Salt = c.String(),
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
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessageId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Message", t => t.MessageId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.MessageId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Question", "AnswersId", "dbo.Answers");
            DropForeignKey("dbo.Questions", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Game", "QuestionsId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "GameId", "dbo.Game");
            DropForeignKey("dbo.Games", "GameId", "dbo.Game");
            DropForeignKey("dbo.Room", "GamesId", "dbo.Games");
            DropForeignKey("dbo.Players", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Messages", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Games", "RoomId", "dbo.Room");
            DropForeignKey("dbo.BannedUsers", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Players", "UserId", "dbo.User");
            DropForeignKey("dbo.Room", "PlayersId", "dbo.Players");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Messages", "MessageId", "dbo.Message");
            DropForeignKey("dbo.Room", "MessagesId", "dbo.Messages");
            DropForeignKey("dbo.BannedUsers", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropForeignKey("dbo.Room", "BannedUsersId", "dbo.BannedUsers");
            DropForeignKey("dbo.Question", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Question");
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.Players", new[] { "RoomId" });
            DropIndex("dbo.Messages", new[] { "RoomId" });
            DropIndex("dbo.Messages", new[] { "MessageId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.BannedUsers", new[] { "RoomId" });
            DropIndex("dbo.BannedUsers", new[] { "UserId" });
            DropIndex("dbo.Room", new[] { "BannedUsersId" });
            DropIndex("dbo.Room", new[] { "MessagesId" });
            DropIndex("dbo.Room", new[] { "GamesId" });
            DropIndex("dbo.Room", new[] { "PlayersId" });
            DropIndex("dbo.Games", new[] { "GameId" });
            DropIndex("dbo.Games", new[] { "RoomId" });
            DropIndex("dbo.Game", new[] { "QuestionsId" });
            DropIndex("dbo.Questions", new[] { "GameId" });
            DropIndex("dbo.Questions", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "AnswersId" });
            DropIndex("dbo.Question", new[] { "CategoryId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Answers", new[] { "AnswerId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropTable("dbo.Players");
            DropTable("dbo.Messages");
            DropTable("dbo.Message");
            DropTable("dbo.User");
            DropTable("dbo.BannedUsers");
            DropTable("dbo.Room");
            DropTable("dbo.Games");
            DropTable("dbo.Game");
            DropTable("dbo.Questions");
            DropTable("dbo.Category");
            DropTable("dbo.Question");
            DropTable("dbo.Answers");
            DropTable("dbo.Answer");
        }
    }
}
