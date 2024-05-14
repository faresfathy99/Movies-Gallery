using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoviesGallery.Shared.Data;
using MoviesGallery.Shared.DTOs;
using MoviesGallery.Shared.Models;
using MoviesGallery.Shared.Models.Response;

namespace MoviesGallery.Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieService(ApplicationDbContext _dbContext, IWebHostEnvironment _webHostEnvironment)
        {
            this._dbContext = _dbContext;
            this._webHostEnvironment = _webHostEnvironment;
        }
        public async Task<ApiResponse<Movie>> AddMovieAsync(MovieDto movieDto)
        {
            var movie = await _dbContext.Movies.Where(e => e.Title == movieDto.Title).FirstOrDefaultAsync();
            if (movie != null)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = false,
                    Message = "Movie with this title already exists",
                    StatusCode = 400
                };
            }
            var newMovie = ConvertFromMovieDto_Add(movieDto);
            await _dbContext.Movies.AddAsync(newMovie);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Movie>
            {
                IsSuccess = true,
                Message = "Movie saved successfully",
                ResponseObject = newMovie,
                StatusCode = 201
             };
        }

        public async Task<ApiResponse<Movie>> DeleteMovieAsync(Guid movieId)
        {
            var movie = await _dbContext.Movies.Where(e => e.Id == movieId).FirstOrDefaultAsync();
            if (movie!=null)
            {
                if (movie.PosterFile != null)
                {
                    DeleteImage(movie.PosterFile);
                }
                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse<Movie>
                {
                    IsSuccess = true,
                    Message = "Movie deleted successfully",
                    StatusCode = 200,
                    ResponseObject = movie 
                };
            }
            return new ApiResponse<Movie>
            {
                IsSuccess = false,
                Message = "Movie not found",
                StatusCode = 404,
            };
        }

        public async Task<ApiResponse<Movie>> GetMovieAsync(Guid movieId)
        {
            var movie = await _dbContext.Movies.Where(e => e.Id == movieId).FirstOrDefaultAsync();
            if (movie != null)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = true,
                    Message = "Movie found successfully",
                    StatusCode = 200,
                    ResponseObject = movie
                };
            }
            return new ApiResponse<Movie>
            {
                IsSuccess = false,
                Message = "Movie not found",
                StatusCode = 404,
            };
        }

        public async Task<ApiResponse<IEnumerable<Movie>>> GetMoviesAsync()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            if (movies.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Movie>>
                {
                    IsSuccess = true,
                    Message = "Empty",
                    StatusCode = 200,
                    ResponseObject = movies
                };
            }
            return new ApiResponse<IEnumerable<Movie>>
            {
                IsSuccess = true,
                Message = "Movies found successfully",
                StatusCode = 200,
                ResponseObject = movies
            };
        }

        public async Task<ApiResponse<Movie>> SearchMovieAsync(string movieName)
        {
            var movie = await _dbContext.Movies.Where(e => e.Title.Contains(movieName.ToLower())).FirstOrDefaultAsync();
            if (movie == null)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = false,
                    Message = "Movie not found",
                    StatusCode = 404,
                };
            }
            return new ApiResponse<Movie>
            {
                IsSuccess = true,
                Message = "Movie found successfully",
                StatusCode = 200,
                ResponseObject = movie
            };
        }

        public async Task<ApiResponse<Movie>> UpdateMovieAsync(MovieDto movieDto)
        {
            if (movieDto.Id == null)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = false,
                    Message = "Movie id must not be null",
                    StatusCode = 400,
                };
            }
            var movie = await _dbContext.Movies.Where(e => e.Id == new Guid (movieDto.Id)).FirstOrDefaultAsync();
            if (movie == null)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = false,
                    Message = "Movie not found",
                    StatusCode = 404,
                };
            }
            if(movieDto.Title == movie.Title)
            {
                return new ApiResponse<Movie>
                {
                    IsSuccess = false,
                    Message = "Movie with this name already exists",
                    StatusCode = 400,
                };
            }
            if (movie.PosterFile != null)
            {
                DeleteImage(movie.PosterFile);
            }
            var updatedMovie = await UpdateMovie(movieDto);
            return new ApiResponse<Movie>
            {
                IsSuccess = true,
                Message = "Movie updated successfully",
                StatusCode= 200,
                ResponseObject = updatedMovie
            };
        }


        private string SaveImageWithUniqueName(IFormFile image)
        {
            var path = @"E:\New folder (4)\MoviesGallery.Api\MoviesGallery.Client\wwwroot\Images\PostersImages\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string uniqueName = Guid.NewGuid().ToString() + "_" + image.FileName;
            using (var stream = new FileStream(Path.Combine(path, uniqueName), FileMode.Create))
            {
                image.CopyToAsync(stream);
                stream.Flush();
            }
            return uniqueName;
        }

        private bool DeleteImage(string uniqueName)
        {
            var path = @"E:\New folder (4)\MoviesGallery.Api\MoviesGallery.Client\wwwroot\Images\PostersImages\";
            var image = Path.Combine(path, uniqueName);
            if(!System.IO.File.Exists(Path.Combine(path, image)))
            {
                return false;
            }
            System.IO.File.Delete(Path.Combine(path, image));
            return true;
        }

        private Movie ConvertFromMovieDto_Add(MovieDto movieDto)
        {
            if (movieDto.PosterFile != null)
            {
                return new Movie
                {
                    Actors = movieDto.Actors,
                    Director = movieDto.Director,
                    Genre = movieDto.Genre,
                    Plot = movieDto.Plot,
                    PosterFile = SaveImageWithUniqueName(movieDto.PosterFile),
                    PosterURL = movieDto.PosterURL,
                    Rating = movieDto.Rating,
                    Title = movieDto.Title.ToLower(),
                    Trailer = movieDto.Trailer,
                    Year = movieDto.Year
                };
            }
            return new Movie
            {
                Actors = movieDto.Actors,
                Director = movieDto.Director,
                Genre = movieDto.Genre,
                Plot = movieDto.Plot,
                PosterURL = movieDto.PosterURL,
                Rating = movieDto.Rating,
                Title = movieDto.Title,
                Trailer = movieDto.Trailer,
                Year = movieDto.Year
            };
        }

        private Movie ConvertFromMovieDto_Update(MovieDto movieDto)
        {
            if (movieDto.Id == null)
            {
                throw new Exception("Movie id must not be null");
            }
            if (movieDto.PosterFile != null)
            {
                return new Movie
                {
                    Id = new Guid(movieDto.Id),
                    Actors = movieDto.Actors,
                    Director = movieDto.Director,
                    Genre = movieDto.Genre,
                    Plot = movieDto.Plot,
                    PosterFile = SaveImageWithUniqueName(movieDto.PosterFile),
                    PosterURL = movieDto.PosterURL,
                    Rating = movieDto.Rating,
                    Title = movieDto.Title,
                    Trailer = movieDto.Trailer,
                    Year = movieDto.Year
                };
            }
            return new Movie
            {
                Actors = movieDto.Actors,
                Director = movieDto.Director,
                Genre = movieDto.Genre,
                Plot = movieDto.Plot,
                PosterURL = movieDto.PosterURL,
                Rating = movieDto.Rating,
                Title = movieDto.Title,
                Trailer = movieDto.Trailer,
                Year = movieDto.Year
            };
        }

        private async Task<Movie> UpdateMovie(MovieDto movieDto)
        {
            var movie = await _dbContext.Movies.Where(e => e.Id == new Guid(movieDto.Id)).FirstOrDefaultAsync();
            movie.Title = movieDto.Title;
            movie.Actors = movieDto.Actors;
            movie.Director = movieDto.Director;
            movie.Genre = movieDto.Genre;
            movie.Plot = movieDto.Plot;
            movie.PosterURL = movieDto.PosterURL;
            movie.Rating = movieDto.Rating;
            movie.Title = movieDto.Title;
            movie.Trailer = movieDto.Trailer;
            movie.Year = movieDto.Year;
            if (movieDto.PosterFile != null)
            {
                movie.PosterFile = SaveImageWithUniqueName(movieDto.PosterFile);
            }
            movie.PosterFile = null;
            await _dbContext.SaveChangesAsync();
            return movie;
        }

    }
}
