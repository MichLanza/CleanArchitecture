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

        public DbSet<ConceptModel> Concepts { get; set; }

        public DbSet<SaleModel> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGameConsoleModel>().ToTable("VideoGameConsole");
            modelBuilder.Entity<SaleModel>().ToTable("Sale");
            modelBuilder.Entity<ConceptModel>().ToTable("Concept");


            modelBuilder.Entity<SaleModel>()
                .HasMany(c => c.Concepts)
                .WithOne()
                .HasForeignKey(c => c.IdSale)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
