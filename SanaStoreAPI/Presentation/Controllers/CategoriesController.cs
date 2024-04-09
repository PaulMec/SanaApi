using DB.Context;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanaStoreAPI.Presentation.Controllers
{
    /// <summary>
    /// API controller for managing product categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SanaStoreContext _context;

        public CategoriesController(SanaStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all product categories.
        /// </summary>
        /// <returns>A list of all product categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDTOs = MapCategoriesToCategoryDto(categories);
            return Ok(categoryDTOs);
        }

        /// <summary>
        /// Retrieves a specific product category by its ID.
        /// </summary>
        /// <param name="id">The ID of the product category to retrieve.</param>
        /// <returns>The product category with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = MapCategoryToCategoryDto(category);

            return Ok(categoryDTO);
        }

        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <param name="category">The product category to create.</param>
        /// <returns>The newly created product category.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, category);
        }

        /// <summary>
        /// Updates an existing product category.
        /// </summary>
        /// <param name="id">The ID of the product category to update.</param>
        /// <param name="category">The updated product category data.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a product category.
        /// </summary>
        /// <param name="id">The ID of the product category to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }

        /// <summary>
        /// Maps a collection of category entities to a list of category DTOs.
        /// </summary>
        /// <param name="categories">The collection of category entities to map.</param>
        /// <returns>A list of mapped category DTOs.</returns>
        private List<CategoryDTO> MapCategoriesToCategoryDto(IEnumerable<Category> categories)
        {
            return categories.Select(MapCategoryToCategoryDto).ToList();
        }

        /// <summary>
        /// Maps a category entity to a category DTO.
        /// </summary>
        /// <param name="category">The category entity to map.</param>
        /// <returns>The mapped category DTO.</returns>
        private CategoryDTO MapCategoryToCategoryDto(Category category)
        {
            return new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
        }
    }
}
