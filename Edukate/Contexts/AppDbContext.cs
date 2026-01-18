using Edukate.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Edukate.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

    }
}
