namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRelations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropIndex("dbo.Answers", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "CategoryId" });
            DropIndex("dbo.Question", new[] { "AnswersId" });
            DropIndex("dbo.Questions", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "GameId" });
            DropIndex("dbo.Game", new[] { "QuestionsId" });
            DropIndex("dbo.Games", new[] { "RoomId" });
            DropIndex("dbo.Games", new[] { "GameId" });
            DropIndex("dbo.Room", new[] { "PlayersId" });
            DropIndex("dbo.Room", new[] { "GamesId" });
            DropIndex("dbo.Room", new[] { "MessagesId" });
            DropIndex("dbo.Room", new[] { "BannedUsersId" });
            DropIndex("dbo.BannedUsers", new[] { "UserId" });
            DropIndex("dbo.BannedUsers", new[] { "RoomId" });
            DropIndex("dbo.Messages", new[] { "MessageId" });
            DropIndex("dbo.Messages", new[] { "RoomId" });
            DropIndex("dbo.Players", new[] { "RoomId" });
            DropIndex("dbo.Players", new[] { "UserId" });
            AlterColumn("dbo.Answer", "UserId", c => c.Guid());
            AlterColumn("dbo.Answers", "AnswerId", c => c.Guid());
            AlterColumn("dbo.Answers", "QuestionId", c => c.Guid());
            AlterColumn("dbo.Question", "CategoryId", c => c.Guid());
            AlterColumn("dbo.Question", "AnswersId", c => c.Guid());
            AlterColumn("dbo.Questions", "QuestionId", c => c.Guid());
            AlterColumn("dbo.Questions", "GameId", c => c.Guid());
            AlterColumn("dbo.Game", "QuestionsId", c => c.Guid());
            AlterColumn("dbo.Games", "RoomId", c => c.Guid());
            AlterColumn("dbo.Games", "GameId", c => c.Guid());
            AlterColumn("dbo.Room", "PlayersId", c => c.Guid());
            AlterColumn("dbo.Room", "GamesId", c => c.Guid());
            AlterColumn("dbo.Room", "MessagesId", c => c.Guid());
            AlterColumn("dbo.Room", "BannedUsersId", c => c.Guid());
            AlterColumn("dbo.BannedUsers", "UserId", c => c.Guid());
            AlterColumn("dbo.BannedUsers", "RoomId", c => c.Guid());
            AlterColumn("dbo.Messages", "MessageId", c => c.Guid());
            AlterColumn("dbo.Messages", "RoomId", c => c.Guid());
            AlterColumn("dbo.Players", "RoomId", c => c.Guid());
            AlterColumn("dbo.Players", "UserId", c => c.Guid());
            CreateIndex("dbo.Answer", "UserId");
            CreateIndex("dbo.Answers", "AnswerId");
            CreateIndex("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Question", "CategoryId");
            CreateIndex("dbo.Question", "AnswersId");
            CreateIndex("dbo.Questions", "QuestionId");
            CreateIndex("dbo.Questions", "GameId");
            CreateIndex("dbo.Game", "QuestionsId");
            CreateIndex("dbo.Games", "RoomId");
            CreateIndex("dbo.Games", "GameId");
            CreateIndex("dbo.Room", "PlayersId");
            CreateIndex("dbo.Room", "GamesId");
            CreateIndex("dbo.Room", "MessagesId");
            CreateIndex("dbo.Room", "BannedUsersId");
            CreateIndex("dbo.BannedUsers", "UserId");
            CreateIndex("dbo.BannedUsers", "RoomId");
            CreateIndex("dbo.Messages", "MessageId");
            CreateIndex("dbo.Messages", "RoomId");
            CreateIndex("dbo.Players", "RoomId");
            CreateIndex("dbo.Players", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.Players", new[] { "RoomId" });
            DropIndex("dbo.Messages", new[] { "RoomId" });
            DropIndex("dbo.Messages", new[] { "MessageId" });
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
            AlterColumn("dbo.Players", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Players", "RoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Messages", "RoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Messages", "MessageId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BannedUsers", "RoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BannedUsers", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Room", "BannedUsersId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Room", "MessagesId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Room", "GamesId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Room", "PlayersId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Games", "GameId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Games", "RoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Game", "QuestionsId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Questions", "GameId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Questions", "QuestionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Question", "AnswersId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Question", "CategoryId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Answers", "QuestionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Answers", "AnswerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Answer", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Players", "UserId");
            CreateIndex("dbo.Players", "RoomId");
            CreateIndex("dbo.Messages", "RoomId");
            CreateIndex("dbo.Messages", "MessageId");
            CreateIndex("dbo.BannedUsers", "RoomId");
            CreateIndex("dbo.BannedUsers", "UserId");
            CreateIndex("dbo.Room", "BannedUsersId");
            CreateIndex("dbo.Room", "MessagesId");
            CreateIndex("dbo.Room", "GamesId");
            CreateIndex("dbo.Room", "PlayersId");
            CreateIndex("dbo.Games", "GameId");
            CreateIndex("dbo.Games", "RoomId");
            CreateIndex("dbo.Game", "QuestionsId");
            CreateIndex("dbo.Questions", "GameId");
            CreateIndex("dbo.Questions", "QuestionId");
            CreateIndex("dbo.Question", "AnswersId");
            CreateIndex("dbo.Question", "CategoryId");
            CreateIndex("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Answers", "AnswerId");
            CreateIndex("dbo.Answer", "UserId");
        }
    }
}
