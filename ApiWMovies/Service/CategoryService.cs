using ApiWMovies.DAL.Dtos;
using ApiWMovies.DAL.Models;
using ApiWMovies.Repository.IRepository;
using ApiWMovies.Service.IService;
using AutoMapper;

namespace ApiWMovies.Service;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryDto>> GetCategoryAsync()
    {
        //Get categories from repository
        var categories = await _categoryRepository.GetCategoryAsync();

        //Map categories to CategoryDto
        return _mapper.Map<ICollection<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryAsync(int id)
    {
        //Get category from repository
        var category = await _categoryRepository.GetCategoryAsync(id);

        if (category == null)
        {
            throw new InvalidOperationException($"No existe una categoria con el id '{id}' ");
        }

        //Map category to CategoryDto
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto categoryCreateDto)
    {
        //Validar que la categoria no exista
        var categoryExist = await CategoryExistsByNameAsync(categoryCreateDto.Name);

        if (categoryExist)
        {
            throw new InvalidOperationException($"Ya existe una categoria con el nombre de '{categoryCreateDto.Name}' ");
        }

        //Mapear el categoryCreateDto a Category

        var category = _mapper.Map<Category>(categoryCreateDto);

        //Crear la categoria en el repositorio
        var Categorycreated = await _categoryRepository.CreateCategoryAsync(category);

        if (!Categorycreated)
        {
            throw new Exception("No se pudo crear la categoria");
        }

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(int id, CategoryUpdateCreateDto categoryUpdateDto)
    {
        var categoryExist = await _categoryRepository.GetCategoryAsync(id);
        if (categoryExist == null)
        {
            throw new InvalidOperationException($"No existe una categoria con el id '{id}' ");
        }

        var CategorynameExist = await CategoryExistsByNameAsync(categoryUpdateDto.Name);
        if (CategorynameExist)
        {
            throw new InvalidOperationException($"Ya existe una categoria con el nombre de '{categoryUpdateDto.Name}' ");
        }

        //Mapear el categoryUpdateDto a Category
        _mapper.Map(categoryUpdateDto, categoryExist);

        //Actualizar la categoria en el repositorio

        var Categoryupdated = await _categoryRepository.UpdateCategoryAsync(categoryExist);

        if (!Categoryupdated)
        {
            throw new Exception("No se pudo actualizar la categoria");
        }

        //Retornar el categoryDto actualizado
        return _mapper.Map<CategoryDto>(categoryExist);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var categoryExist = await _categoryRepository.GetCategoryAsync(id);

        if (categoryExist == null)
        {
            throw new InvalidOperationException($"No existe una categoria con el id '{id}' ");
        }

        //Eliminar la categoria en el repositorio

        var deleteCategory = await _categoryRepository.DeleteCategoryAsync(id);

        if (!deleteCategory)
        {
            throw new Exception("No se pudo eliminar la categoria");
        }

        return deleteCategory;
    }

    public async Task<bool> CategoryExistsByIdAsync(int id) => await _categoryRepository.CategoryExistsByIdAsync(id);

    public async Task<bool> CategoryExistsByNameAsync(string name) => await _categoryRepository.CategoryExistsByNameAsync(name);
}