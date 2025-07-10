using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken token)
        {

            var movie = request.MapToMovie();
            await _movieService.CreateAsync(movie, token);
            return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie.MapToResponse());
        }
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var movies = await _movieService.GetAllAsync(token);
            var moviesResponse = movies.MapToResponse();
            return Ok(moviesResponse);
        }
        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug, CancellationToken token)
        {
            Movie? movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieService.GetByIdAsync(id, token)
            : await _movieService.GetBySlugAsync(idOrSlug, token);

            if (movie is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie.MapToResponse());
            }
        }
        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request, CancellationToken token)
        {
            var movie = request.MapToMovie(id);
            var updatedMovie = await _movieService.UpdateAsync(movie, token);
            if (updatedMovie == null)
            {
                return NotFound();
            }
            else
            {
                var response = updatedMovie.MapToResponse();
                return Ok(response);
            }
        }
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            bool deleted = await _movieService.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }
            else
            {                
                return Ok();
            }
        }

    }
}
