using ApiWMovies.DAL.Dtos;

namespace ApiWMovies.Service.IService
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetMovieAsync(); //return a list of movies

        Task<MovieDto> GetMovieAsync(int id);// return a single movie by id

        Task<MovieDto> CreateMovieAsync(MovieUpdateCreateDto movieCreateDto);// create a new movie

        Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateCreateDto movieUpdateDto);// update an existing movie

        Task<bool> DeleteMovieAsync(int id);// delete a movie by id
    }
}