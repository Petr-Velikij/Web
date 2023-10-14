using Microsoft.EntityFrameworkCore;
using WebTutorCore.Data.Models;

namespace WebTutorCore.Data
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public AppContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WebTutorCore.db");
        }
    }
}
