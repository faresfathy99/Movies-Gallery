

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MoviesGallery.Shared.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Director { get; set; } = null!;

        [Required]
        public string Actors { get; set; } = null!;

        [Required]
        public string Plot { get; set; } = null!;

        [Required]
        public string? PosterURL { get; set; }

        public string? PosterFile { get; set; }

        [Required]
        public string Trailer { get; set; } = null!;
    }
}
