using ApiWMovies.DAL.Dtos;
using ApiWMovies.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ApiWMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMovieAsinc()
        {
            var getMovie = await _movieService.GetMovieAsync();
            return Ok(getMovie);
        }

        [HttpGet("{id:int}", Name = "GetMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> GetMoviesync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieAsync(id);
                return Ok(movie);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No existe"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }

        [HttpPost(Name = "CreateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieUpdateCreateDto movieCreateDto)
        {
            //Protetect the model integrity

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Call the service to create the movie
                var createMovie = await _movieService.CreateMovieAsync(movieCreateDto);

                //Return the 201 Created

                return CreatedAtRoute("GetMovieAsync", //Route name
                    new { id = createMovie.Id }, //Route parameters
                    createMovie);//Return object created
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                //Caprure the exception 409 Conflict  when the movie name already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex1)
            {
                //Capture any other exception 500 Internal Server Error when the movies cannot be created
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> UpdateMovieAsync(int id, [FromBody] MovieUpdateCreateDto movieUpdateDto)
        {
            //Protetect the model integrity

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Call the service to update the movie
                var updateMovie = await _movieService.UpdateMovieAsync(id, movieUpdateDto);

                //Return the updated category
                return Ok(updateMovie);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                //Caprure the exception 409 Conflict  when the movie name already exists
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex1) when (ex1.Message.Contains("No existe"))
            {
                //Capture the exception 404 Not Found when the movie id does not exist
                return NotFound(ex1.Message);
            }
            catch (Exception ex2)
            {
                //Capture any other exception 500 Internal Server Error when the movies cannot be updated
                return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteMoviesync(int id)
        {
            try
            {
                //Call the service to delete the movie for the id
                var deleteMovie = await _movieService.DeleteMovieAsync(id);

                //return the 200 OK with the delete result
                return Ok(deleteMovie);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No existe"))
            {
                //Capture the exception 404 Not Found when the movie id does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex2)
            {
                //Capture any other exception 500 Internal Server Error when the movies cannot be deleted
                return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
            }
        }
    }
}