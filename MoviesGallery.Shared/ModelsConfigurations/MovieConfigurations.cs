
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesGallery.Shared.Models;

namespace MoviesGallery.Shared.ModelsConfigurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Title).IsUnique();
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Trailer).IsRequired();
            builder.Property(e => e.Year).IsRequired();
            builder.Property(e => e.PosterURL).IsRequired();
            builder.Property(e => e.Plot).IsRequired();
            builder.Property(e => e.Genre).IsRequired();
            builder.Property(e => e.Rating).IsRequired();
            builder.Property(e => e.Actors).IsRequired();
        }
    }
}
