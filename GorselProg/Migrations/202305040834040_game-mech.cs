namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gamemech : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        SentTime = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false),
                        AnswerText = c.String(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Admin_Id = c.Int(),
                        CurrentGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Admin_Id)
                .ForeignKey("dbo.Games", t => t.CurrentGame_Id)
                .Index(t => t.Admin_Id)
                .Index(t => t.CurrentGame_Id);
            
            AddColumn("dbo.Users", "Room_Id", c => c.Int());
            AddColumn("dbo.Users", "Room_Id1", c => c.Int());
            CreateIndex("dbo.Users", "Room_Id");
            CreateIndex("dbo.Users", "Room_Id1");
            AddForeignKey("dbo.Users", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.Users", "Room_Id1", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Room_Id1", "dbo.Rooms");
            DropForeignKey("dbo.Games", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "CurrentGame_Id", "dbo.Games");
            DropForeignKey("dbo.Chats", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Users", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "Admin_Id", "dbo.Users");
            DropForeignKey("dbo.Questions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Categories", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Chats", "User_Id", "dbo.Users");
            DropIndex("dbo.Rooms", new[] { "CurrentGame_Id" });
            DropIndex("dbo.Rooms", new[] { "Admin_Id" });
            DropIndex("dbo.Questions", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Room_Id" });
            DropIndex("dbo.Users", new[] { "Room_Id1" });
            DropIndex("dbo.Users", new[] { "Room_Id" });
            DropIndex("dbo.Chats", new[] { "Room_Id" });
            DropIndex("dbo.Chats", new[] { "User_Id" });
            DropIndex("dbo.Categories", new[] { "Question_Id" });
            DropColumn("dbo.Users", "Room_Id1");
            DropColumn("dbo.Users", "Room_Id");
            DropTable("dbo.Rooms");
            DropTable("dbo.Questions");
            DropTable("dbo.Games");
            DropTable("dbo.Chats");
            DropTable("dbo.Categories");
        }
    }
}
