using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }        

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set;}
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Property>().HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasKey(p => new { p.Id });

            modelBuilder.Entity<Photo>().HasKey(p => new { p.Id });

            modelBuilder.Entity<Rent>().HasKey(k => new { k.UserId, k.PropertyId });

            modelBuilder.Entity<Rent>()
                .HasOne(p => p.User)
                .WithMany(p => p.Rents)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(m => m.MessageSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Property)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}