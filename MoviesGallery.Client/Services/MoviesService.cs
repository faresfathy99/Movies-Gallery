using Microsoft.AspNetCore.Http;
using MoviesGallery.Shared.DTOs;
using MoviesGallery.Shared.Models;
using System.Net.Http.Json;

namespace MoviesGallery.Client.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly HttpClient _httpClient;
        public MoviesService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }
        public async Task AddMovieAsync(MovieDto newMovie)
        {
            await _httpClient.PostAsJsonAsync("addMovie", newMovie);
        }

        public async Task DeleteMovieByIdAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"deleteMovie/{id}");
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"getMovie/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Movie>();
                }
                throw new Exception($"Status code {response.StatusCode} Message {response.Content.ReadAsStringAsync()}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("getMovies");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<Movie>>();
                }
                throw new Exception($"Status code {response.StatusCode} Message {response.Content.ReadAsStringAsync()}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Movie> SearchAsync(string word)
        {
            try
            {
                var response = await _httpClient.GetAsync($"search/{word}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Movie>();
                }
                throw new Exception($"Status code {response.StatusCode} Message {response.Content.ReadAsStringAsync()}");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task UpdateMovieAsync(MovieDto updatedMovie)
        {
            await _httpClient.PutAsJsonAsync("updateMovie", updatedMovie);
        }
    }
}
