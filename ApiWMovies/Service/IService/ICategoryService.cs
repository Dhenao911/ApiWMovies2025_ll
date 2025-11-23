using ApiWMovies.DAL.Dtos;

namespace ApiWMovies.Service.IService
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetCategoryAsync(); //return a list of categories

        Task<CategoryDto> GetCategoryAsync(int id);// return a single category by id

        Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto categoryCreateDto);// create a new category

        Task<CategoryDto> UpdateCategoryAsync(int id, CategoryUpdateCreateDto categoryUpdateDto);// update an existing category

        Task<bool> CategoryExistsByIdAsync(int id);// check if category exists by id

        Task<bool> CategoryExistsByNameAsync(string name);// check if category exists by name

        Task<bool> DeleteCategoryAsync(int id);// delete a category by id
    }
}