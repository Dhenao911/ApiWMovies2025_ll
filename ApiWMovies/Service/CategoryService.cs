using ApiWMovies.DAL.Dtos;
using ApiWMovies.Repository.IRepository;
using ApiWMovies.Service.IService;
using AutoMapper;

namespace ApiWMovies.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
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

        public async Task<ICollection<CategoryDto>> GetCategoryAsync()
        {
            var categories =  await  _categoryRepository.GetCategoryAsync();

            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }


        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public Task<CategoryDto> UpdateCategoryAsync(int id, CategoryUpdateCreateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
