using ApiWMovies.DAL.Dtos;
using ApiWMovies.Repository.IRepository;
using ApiWMovies.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<CategoryDto>>> GeCategoryAsync()
        {
            var categories = await _categoryService.GetCategoryAsync();

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryAsync")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category==null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}