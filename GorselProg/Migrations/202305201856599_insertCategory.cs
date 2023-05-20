namespace GorselProg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class insertCategory : DbMigration
    {
        public override void Up()
        {
            // Veri eklemek istediğiniz örnekleri oluşturunuz
            var categories = new[]
            {
            new { Id = Guid.NewGuid(), Name = "Spor",Index = 0 },
            new { Id = Guid.NewGuid(), Name = "Tarih",Index = 1 },
            new { Id = Guid.NewGuid(), Name = "Sanat",Index = 2 },
            new { Id = Guid.NewGuid(), Name = "Bilim",Index = 3 },
            new { Id = Guid.NewGuid(), Name = "Eğlence",Index = 4 },

        };

            // Kategorileri Category tablosuna ekleyin
            foreach (var category in categories)
            {
                Sql($"INSERT INTO Category (Id, Name) VALUES ('{category.Id}', '{category.Name}')");
            }
        }

        public override void Down()
        {
            // Veriyi geri almak için gerekli olan SQL kodunu burada tanımlayabilirsiniz
            Sql("DELETE FROM Category");
        }
    }
}