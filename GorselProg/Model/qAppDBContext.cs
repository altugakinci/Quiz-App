using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Model
{
    class qAppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms  { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
