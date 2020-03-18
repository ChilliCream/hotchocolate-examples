using Microsoft.EntityFrameworkCore;
using Chat.Server.Messages;
using Chat.Server.People;
using Chat.Server.Users;

#nullable disable

namespace Chat.Server
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=chat.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonToFriend>()
                .HasKey(bc => new { bc.PersionId, bc.FriendId });

            modelBuilder.Entity<PersonToFriend>()
                .HasOne(bc => bc.Person)
                .WithMany(b => b.Friends)
                .HasForeignKey(bc => bc.PersionId);

            modelBuilder.Entity<PersonToFriend>()
                .HasOne(bc => bc.Friend)
                .WithMany(b => b.FriendOf)
                .HasForeignKey(bc => bc.FriendId);

        }
    }
}