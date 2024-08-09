using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Parrot.Model;

namespace Parrot.Data;
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<ModerationLog> ModerationLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Model.Thread> Threads { get; set; }
        public DbSet <UserRole> UserRoles { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
