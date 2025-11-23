using ApiWMovies.DAL.Models;

namespace ApiWMovies.Repository.IRepository;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetCategoryAsync(); //return a list of categories

    Task<Category> GetCategoryAsync(int id);// return a single category by id

    Task<bool> CategoryExistsByIdAsync(int id);// check if category exists by id

    Task<bool> CategoryExistsByNameAsync(string name);// check if category exists by name

    Task<bool> CreateCategoryAsync(Category category);// create a new category

    Task<bool> UpdateCategoryAsync(Category category);// update an existing category

    Task<bool> DeleteCategoryAsync(int id);// delete a category by id
}