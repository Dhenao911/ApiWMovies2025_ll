using ApiWMovies.DAL.Dtos;
using ApiWMovies.DAL.Models;
using ApiWMovies.Repository.IRepository;
using ApiWMovies.Service.IService;
using AutoMapper;

namespace ApiWMovies.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<MovieDto>> GetMovieAsync()
        {
            var movies = await _movieRepository.GetMovieAsync();

            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            if (movie == null)
            {
                throw new InvalidOperationException($"No existe una película con el id {id}.");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto)
        {
            // Validar si la película ya existe por nombre
            var movieExist = await _movieRepository.MovieExistsByNameAsync(movieCreateDto.Name);
            if (movieExist)
            {
                throw new InvalidOperationException($"Ya existe una película con el nombre {movieCreateDto.Name}.");
            }
            // Mapear el movieCreateDto a Movie
            var movie = _mapper.Map<Movie>(movieCreateDto);
            //Crear la película en el repositorio
            var movieCreated = await _movieRepository.CreateMovieAsync(movie);
            if (!movieCreated)
            {
                throw new InvalidOperationException("Error al crear la película.");
            }
            // Mapear la película creada a MovieDto y retornarla
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movieExist = await _movieRepository.GetMovieAsync(id);
            if (movieExist == null)
            {
                throw new InvalidOperationException($"No existe una pelicula con Id '{id}'");
            }
            var deleteMovie = await _movieRepository.DeleteMovieAsync(id);
            if (!deleteMovie)
            {
                throw new Exception("No se puede eliminar la pelicula");
            }
            return deleteMovie;
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            var movieExist = await _movieRepository.GetMovieAsync(id);
            if (movieExist == null)
            {
                throw new InvalidOperationException($"No existe una pelicula con Id '{id}'");
            }

            var movieNameExist = await _movieRepository.MovieExistsByNameAsync(movieUpdateDto.Name);
            if (movieNameExist)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre de '{movieUpdateDto.Name}'");
            }

            //Mapeamos el movieUpdateDto a movie

            _mapper.Map(movieUpdateDto, movieExist);

            //Actualizamos la movie en el repositorio

            var updateMovie = await _movieRepository.UpdateMovieAsync(movieExist);

            if (!updateMovie)
            {
                throw new Exception("No se puede actualizar la pelicula");
            }

            //retornamos el movieDto

            return _mapper.Map<MovieDto>(movieExist);
        }
    }
}