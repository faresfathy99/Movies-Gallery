using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesGallery.Api.Services;
using MoviesGallery.Shared.Data;
using MoviesGallery.Shared.DTOs;
using MoviesGallery.Shared.Models;
using MoviesGallery.Shared.Models.Response;

namespace MoviesGallery.Api.Controllers
{

    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService _movieService)
        {
            this._movieService = _movieService;
        }
        [HttpPost("addMovie")]
        public async Task<IActionResult> AddMovieAsync([FromBody]MovieDto movieDto)
        {
            try
            {
                var response = await _movieService.AddMovieAsync(movieDto);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> SearchAsync([FromRoute]string title)
        {
            try
            {
                var response = await _movieService.SearchMovieAsync(title);
                return Ok(response.ResponseObject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("updateMovie")]
        public async Task<IActionResult> UpdateMovieAsync([FromBody]MovieDto movieDto)
        {
            try
            {
                var response = await _movieService.UpdateMovieAsync(movieDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("deleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovieAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _movieService.DeleteMovieAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getMovie/{id}")]
        public async Task<IActionResult> GetMovieAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _movieService.GetMovieAsync(id);
                return Ok(response.ResponseObject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getMovies")]
        public async Task<ActionResult<List<Movie>>> GetMoviesAsync()
        {
            try
            {
                var response = await _movieService.GetMoviesAsync();
                return Ok(response.ResponseObject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

    }
}
