using ApiWMovies.DAL;
using ApiWMovies.DAL.Models;
using ApiWMovies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiWMovies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Movie>> GetMovieAsync()
        {
            return await _context.Movies
                         .AsNoTracking()
                         .OrderBy(m => m.Name)
                         .ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            return await _context.Movies
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<bool> CreateMovieAsync(Movie movie)
        {
            movie.CreatedDate = DateTime.UtcNow;
            await _context.Movies.AddAsync(movie);
            return await SaveAsync();
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movieDelete = await GetMovieAsync(id);
            if (movieDelete == null)
            {
                return false;
            }

            _context.Movies.Remove(movieDelete);
            return await SaveAsync();
        }

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            movie.UpdateDate = DateTime.UtcNow;
            _context.Movies.Update(movie);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}