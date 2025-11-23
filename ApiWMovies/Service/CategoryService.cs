using ApiWMovies.DAL.Dtos;
using ApiWMovies.Service.IService;

namespace ApiWMovies.Service
{
    public class CategoryService : ICategoryService
    {
        public Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto categoryCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CategoryDto>> GetCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> UpdateCategoryAsync(int id, CategoryUpdateCreateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
