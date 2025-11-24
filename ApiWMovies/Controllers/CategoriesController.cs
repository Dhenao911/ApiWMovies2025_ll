using ApiWMovies.DAL.Dtos;
using ApiWMovies.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ApiWMovies.Controllers;

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
        try
        {
            var categoryDto = await _categoryService.GetCategoryAsync(id);

            return Ok(categoryDto);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("No existe"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost(Name = "CategoryCreateAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CategoryUpdateCreateDto categoryCreateDto)
    {
        //Protetect the model integrity
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var CategoryDto = await _categoryService.CreateCategoryAsync(categoryCreateDto);

            //Return 201 create
            return CreatedAtRoute("GetCategoryAsync" //Route name
                , new { id = CategoryDto.Id },//Route parameter
                CategoryDto);//Return Object
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}", Name = "UpdateCategoryAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(int id, [FromBody] CategoryUpdateCreateDto categoryUpdateDto)
    {
        //Protetect the model integrity
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var categoryUpdated = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);

            //Return 200Ok
            return Ok(categoryUpdated);
        }
        //return  NoFound() when the category id does not exist
        catch (InvalidOperationException ex1) when (ex1.Message.Contains("No existe"))
        {
            return NotFound(ex1.Message);
        }
        //return  Conflict When the category name already exists
        catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
        {
            return Conflict(ex.Message);
        }
        //Retur 500 InternalServerError when the category cannot be updated
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteCategoryAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
            return Ok(deletedCategory);
        }
        
        catch (InvalidOperationException ex1) when (ex1.Message.Contains("No existe"))
        {
            return NotFound(ex1.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}