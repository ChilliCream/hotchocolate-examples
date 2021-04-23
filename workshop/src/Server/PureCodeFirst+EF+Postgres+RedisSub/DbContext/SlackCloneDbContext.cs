using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace SlackClone.Models
{
    public class SlackCloneDbContext : DbContext
    {

        public SlackCloneDbContext(DbContextOptions<SlackCloneDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelMessage> ChannelMessages { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<ChannelMember> ChannelMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChannelMember>()
                .HasKey(o => new { o.ChannelId, o.MemberEmail });
            modelBuilder.Entity<ChannelMember>()
                .HasOne(se => se.Channel)
                .WithMany(s => s.Members)
                .HasForeignKey(se => se.ChannelId);
            modelBuilder.Entity<ChannelMember>()
                .HasOne(se => se.Member)
                .WithMany(s => s.Channels)
                .HasForeignKey(se => se.MemberEmail);

            modelBuilder.Entity<Channel>().HasData(
                new Channel { Id = Guid.NewGuid(),  Name = "general", Description = "Ask about general items", CreatedAt = DateTime.Now, CreatedByEmail = "test@test.com" }
                );
        }
    }
}