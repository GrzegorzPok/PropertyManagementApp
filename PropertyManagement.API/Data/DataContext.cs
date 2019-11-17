using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => new { p.Id });
            modelBuilder.Entity<User>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Photo>().HasKey(p => new { p.Id });
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set;}
        public DbSet<Property> Properties { get; set; }
    }
}