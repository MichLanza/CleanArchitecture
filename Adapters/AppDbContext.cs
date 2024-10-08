using Microsoft.EntityFrameworkCore;
using ModelAdapters;

namespace DataAdapters
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<VideoGameConsoleModel> VideoGameConsoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGameConsoleModel>().ToTable("VideoGameConsole");
            base.OnModelCreating(modelBuilder);
        }
    }
}
