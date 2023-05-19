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
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<BannedUser> BannedUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameQuestion> GameQuestions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.Game)
                .HasForeignKey(e => e.CurrentGameId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.Password)
            //    .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AdminId);
        }
    }
}
