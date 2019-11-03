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
    }
}