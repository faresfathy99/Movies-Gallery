

using Microsoft.EntityFrameworkCore;
using MoviesGallery.Shared.Models;
using MoviesGallery.Shared.ModelsConfigurations;

namespace MoviesGallery.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MovieConfigurations());
        }

        public DbSet<Movie> Movies { get; set; }

    }
}
