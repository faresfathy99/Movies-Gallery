using MoviesGallery.Shared.DTOs;
using MoviesGallery.Shared.Models;

namespace MoviesGallery.Client.Services
{
    public interface IMoviesService
    {
        Task AddMovieAsync(MovieDto newMovie);
        Task UpdateMovieAsync(MovieDto updatedMovie);
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task<Movie> SearchAsync(string word);
        Task DeleteMovieByIdAsync(Guid id);
        Task<IEnumerable<Movie>> GetMoviesAsync();
    }
}
