namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Games", "GameId", "dbo.Game");
            DropForeignKey("dbo.Room", "BannedUsersId", "dbo.BannedUsers");
            DropForeignKey("dbo.Messages", "MessageId", "dbo.Message");
            DropForeignKey("dbo.Room", "MessagesId", "dbo.Messages");
            DropForeignKey("dbo.Room", "PlayersId", "dbo.Players");
            DropForeignKey("dbo.Game", "QuestionsId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "AnswersId", "dbo.Answers");
            DropIndex("dbo.Answers", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "AnswersId" });
            DropIndex("dbo.Questions", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "GameId" });
            DropIndex("dbo.Game", new[] { "QuestionsId" });
            DropIndex("dbo.Games", new[] { "RoomId" });
            DropIndex("dbo.Games", new[] { "GameId" });
            DropIndex("dbo.Room", new[] { "PlayersId" });
            DropIndex("dbo.Room", new[] { "MessagesId" });
            DropIndex("dbo.Room", new[] { "BannedUsersId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "MessageId" });
            DropIndex("dbo.Messages", new[] { "RoomId" });
            DropColumn("dbo.Room", "CurrentGameId");
            RenameColumn(table: "dbo.Room", name: "GamesId", newName: "CurrentGameId");
            RenameIndex(table: "dbo.Room", name: "IX_GamesId", newName: "IX_CurrentGameId");
            AddColumn("dbo.Answer", "QuestionId", c => c.Guid());
            AddColumn("dbo.Answer", "GameId", c => c.Guid());
            AddColumn("dbo.Question", "GameId", c => c.Guid(nullable: false));
            AddColumn("dbo.Game", "RoomId", c => c.Guid());
            AddColumn("dbo.Message", "RoomId", c => c.Guid());
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Room", "Code", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "Email", c => c.String(maxLength: 255));
            AlterColumn("dbo.User", "Salt", c => c.String(nullable: false));
            AlterColumn("dbo.Message", "MessageText", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Message", "UserId", c => c.Guid());
            CreateIndex("dbo.Answer", "QuestionId");
            CreateIndex("dbo.Answer", "GameId");
            CreateIndex("dbo.Game", "RoomId");
            CreateIndex("dbo.Question", "GameId");
            CreateIndex("dbo.Room", "AdminId");
            CreateIndex("dbo.Message", "UserId");
            CreateIndex("dbo.Message", "RoomId");
            AddForeignKey("dbo.Answer", "GameId", "dbo.Game", "Id");
            AddForeignKey("dbo.Room", "AdminId", "dbo.User", "Id");
            DropColumn("dbo.Question", "AnswersId");
            DropColumn("dbo.Game", "QuestionsId");
            DropColumn("dbo.Room", "PlayersId");
            DropColumn("dbo.Room", "MessagesId");
            DropColumn("dbo.Room", "BannedUsersId");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Games");
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessageId = c.Guid(),
                        RoomId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomId = c.Guid(),
                        GameId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionId = c.Guid(),
                        GameId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AnswerId = c.Guid(),
                        QuestionId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Room", "BannedUsersId", c => c.Guid());
            AddColumn("dbo.Room", "MessagesId", c => c.Guid());
            AddColumn("dbo.Room", "PlayersId", c => c.Guid());
            AddColumn("dbo.Game", "QuestionsId", c => c.Guid());
            AddColumn("dbo.Question", "AnswersId", c => c.Guid());
            DropForeignKey("dbo.Room", "AdminId", "dbo.User");
            DropForeignKey("dbo.Answer", "GameId", "dbo.Game");
            DropIndex("dbo.Message", new[] { "RoomId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Room", new[] { "AdminId" });
            DropIndex("dbo.Question", new[] { "GameId" });
            DropIndex("dbo.Game", new[] { "RoomId" });
            DropIndex("dbo.Answer", new[] { "GameId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            AlterColumn("dbo.Message", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "MessageText", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Salt", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Message", "RoomId");
            DropColumn("dbo.Game", "RoomId");
            DropColumn("dbo.Question", "GameId");
            DropColumn("dbo.Answer", "GameId");
            DropColumn("dbo.Answer", "QuestionId");
            RenameIndex(table: "dbo.Room", name: "IX_CurrentGameId", newName: "IX_GamesId");
            RenameColumn(table: "dbo.Room", name: "CurrentGameId", newName: "GamesId");
            AddColumn("dbo.Room", "CurrentGameId", c => c.Guid());
            CreateIndex("dbo.Messages", "RoomId");
            CreateIndex("dbo.Messages", "MessageId");
            CreateIndex("dbo.Message", "UserId");
            CreateIndex("dbo.Room", "BannedUsersId");
            CreateIndex("dbo.Room", "MessagesId");
            CreateIndex("dbo.Room", "PlayersId");
            CreateIndex("dbo.Games", "GameId");
            CreateIndex("dbo.Games", "RoomId");
            CreateIndex("dbo.Game", "QuestionsId");
            CreateIndex("dbo.Questions", "GameId");
            CreateIndex("dbo.Questions", "QuestionId");
            CreateIndex("dbo.Question", "AnswersId");
            CreateIndex("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Answers", "AnswerId");
            AddForeignKey("dbo.Question", "AnswersId", "dbo.Answers", "Id");
            AddForeignKey("dbo.Questions", "QuestionId", "dbo.Question", "Id");
            AddForeignKey("dbo.Game", "QuestionsId", "dbo.Questions", "Id");
            AddForeignKey("dbo.Room", "PlayersId", "dbo.Players", "Id");
            AddForeignKey("dbo.Room", "MessagesId", "dbo.Messages", "Id");
            AddForeignKey("dbo.Messages", "MessageId", "dbo.Message", "Id");
            AddForeignKey("dbo.Room", "BannedUsersId", "dbo.BannedUsers", "Id");
            AddForeignKey("dbo.Games", "GameId", "dbo.Game", "Id");
            AddForeignKey("dbo.Answers", "AnswerId", "dbo.Answer", "Id");
        }
    }
}
