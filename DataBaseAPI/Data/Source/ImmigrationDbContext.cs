using Microsoft.EntityFrameworkCore;
using DataBaseAPI.Data.Models;

namespace DataBaseAPI.Data
{
    public class ImmigrationDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public ImmigrationDbContext() : base()
        {
        }

        public ImmigrationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
