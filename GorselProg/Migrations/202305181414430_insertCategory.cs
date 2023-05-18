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
            new { Id = Guid.NewGuid(), Name = "Spor" },
            new { Id = Guid.NewGuid(), Name = "Tarih" },
            new { Id = Guid.NewGuid(), Name = "Sanat" },
            new { Id = Guid.NewGuid(), Name = "Bilim" },
            new { Id = Guid.NewGuid(), Name = "Eğlence" },

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