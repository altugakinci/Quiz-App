namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Name) VALUES ('Spor')");
            Sql("INSERT INTO Categories (Name) VALUES ('Tarih')");
            Sql("INSERT INTO Categories (Name) VALUES ('Sanat')");
            Sql("INSERT INTO Categories (Name) VALUES ('Bilim')");
            Sql("INSERT INTO Categories (Name) VALUES ('Eğlence')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Name IN ('Spor', 'Tarih', 'Sanat', 'Bilim', 'Eğlence')");
        }
    }
}
