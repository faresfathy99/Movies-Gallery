using MoviesGallery.Shared.DTOs;
using MoviesGallery.Shared.Models;
using MoviesGallery.Shared.Models.Response;

namespace MoviesGallery.Api.Services
{
    public interface IMovieService
    {
        Task<ApiResponse<Movie>> AddMovieAsync(MovieDto movieDto);
        Task<ApiResponse<Movie>> UpdateMovieAsync(MovieDto movieDto);
        Task<ApiResponse<Movie>> GetMovieAsync(Guid movieId);
        Task<ApiResponse<Movie>> DeleteMovieAsync(Guid movieId);
        Task<ApiResponse<Movie>> SearchMovieAsync(string movieName);
        Task<ApiResponse<IEnumerable<Movie>>> GetMoviesAsync();
    }
}
