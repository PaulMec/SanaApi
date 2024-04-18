using Microsoft.AspNetCore.Mvc;
using SanaStoreAPI.Infrastructure.Services.Interfaces;
using SanaStoreAPI.Domain.Models;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Constructor that initializes the CategoriesController with the required service.
    /// </summary>
    /// <param name="categoriesService">Service for category operations.</param>
    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>An IActionResult containing all categories or an error message if an exception occurs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await _categoriesService.GetCategories();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>An IActionResult containing the category if found or NotFound if not found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _categoriesService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Creates a new category based on the data provided.
    /// </summary>
    /// <param name="categoryDto">The data transfer object containing the category data.</param>
    /// <returns>A CreatedAtActionResult with the created category or an error status if an exception occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDto)
    {
        try
        {
            var createdCategory = await _categoriesService.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(Get), new { id = createdCategory.CategoryID }, createdCategory);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing category identified by its ID with new data.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="categoryDto">The updated data for the category.</param>
    /// <returns>NoContent if successful, NotFound if the category does not exist, or an error status if an exception occurs.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
    {
        try
        {
            var success = await _categoriesService.UpdateCategory(id, categoryDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>NoContent if the deletion is successful, NotFound if the category does not exist, or an error status if an exception occurs.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _categoriesService.DeleteCategory(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
