

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.Shared.DTOs
{
    public class MovieDto
    {

        public string? Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        [Range(1900, 2024)]
        public int Year { get; set; }

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        [Range(1,10)]
        public int Rating { get; set; }

        [Required]
        public string Director { get; set; } = null!;

        [Required]
        public string Actors { get; set; } = null!;

        [Required]
        public string Plot { get; set; } = null!;

        [Required]
        public string? PosterURL { get; set; }

        public IFormFile? PosterFile { get; set; }

        [Required]
        public string Trailer { get; set; } = null!;
    }
}
