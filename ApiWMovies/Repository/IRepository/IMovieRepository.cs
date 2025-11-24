using ApiWMovies.DAL.Models;

namespace ApiWMovies.Repository.IRepository;

public interface IMovieRepository
{
    Task<ICollection<Movie>> GetMovieAsync();

    Task<Movie> GetMovieAsync(int id);

    Task<bool> MovieExistsByIdAsync(int id);

    Task<bool> MovieExistsByNameAsync(string title);

    Task<bool> CreateMovieAsync(Movie movie);

    Task<bool> UpdateMovieAsync(Movie movie);

    Task<bool> DeleteMovieAsync(int id);
}