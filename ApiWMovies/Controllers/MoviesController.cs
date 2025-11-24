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
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateDto movieCreateDto)
        {
            //Salvarguardar la integridad del modelo

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Llamar al servicio para crear la categoría
                var createMovie = await _movieService.CreateMovieAsync(movieCreateDto);

                //Vamos a retornar un 201 Created con la ruta para obtener la categoria creada

                return CreatedAtRoute("GetMovieAsync", //Nombre de la ruta
                    new { id = createMovie.Id }, //Parametros de la ruta
                    createMovie);//Objeto a retornar
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> UpdateMovieAsync(int id, [FromBody] MovieUpdateDto movieUpdateDto)
        {
            //Salvarguardar la integridad del modelo

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateMovie = await _movieService.UpdateMovieAsync(id, movieUpdateDto);
                return Ok(updateMovie);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex1) when (ex1.Message.Contains("No existe"))
            {
                return NotFound(ex1.Message);
            }
            catch (Exception ex2)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
            }
        }

        [HttpDelete("{id}",Name ="DeleteMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult>DeleteMoviesync(int id)
        {
            try
            {
                var deleteMovie = await _movieService.DeleteMovieAsync(id);
                return Ok(deleteMovie);
            }
            catch(InvalidOperationException ex) when (ex.Message.Contains("No existe"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex2)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
            }
        }
    }
}