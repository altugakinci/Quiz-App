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
        public virtual DbSet<Answer1> Answers1 { get; set; }
        public virtual DbSet<BannedUser> BannedUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Game1> Games1 { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Message1> Messages1 { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Question1> Questions1 { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer1>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Answer)
                .HasForeignKey(e => e.AnswersId);

            modelBuilder.Entity<BannedUser>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.BannedUser)
                .HasForeignKey(e => e.BannedUsersId);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Game)
                .HasForeignKey(e => e.GameId);

            modelBuilder.Entity<Game1>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.Game)
                .HasForeignKey(e => e.GamesId);

            modelBuilder.Entity<Message1>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.Message)
                .HasForeignKey(e => e.MessagesId);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.Player)
                .HasForeignKey(e => e.PlayersId);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<Question1>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuestionsId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.BannedUsers)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Messages)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Players)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.password)
            //    .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Rooms)
            //    .WithOptional(e => e.User)
            //    .HasForeignKey(e => e.AdminId);
        }
    }
}
