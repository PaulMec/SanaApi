using DB.Context;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Data
{
    /// <summary>
    /// Repository for managing product categories.
    /// </summary>
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly SanaStoreContext _context;

        public CategoriesRepository(SanaStoreContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves all product categories.
        /// </summary>
        /// <returns>A list of all product categories.</returns>
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDTOs = MapCategoriesToCategoryDto(categories);
            return categoryDTOs;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves a specific product category by its ID.
        /// </summary>
        /// <param name="id">The ID of the product category to retrieve.</param>
        /// <returns>The product category with the specified ID.</returns>
        public async Task<CategoryDTO> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            var categoryDTO = MapCategoryToCategoryDto(category);

            return categoryDTO;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <param name="category">The product category to create.</param>
        /// <returns>The newly created product category.</returns>
        public async Task<CategoryDTO> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return MapCategoryToCategoryDto(category);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Updates an existing product category.
        /// </summary>
        /// <param name="id">The ID of the product category to update.</param>
        /// <param name="category">The updated product category data.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return false;
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }


        /// <inheritdoc/>
        /// <summary>
        /// Deletes a product category.
        /// </summary>
        /// <param name="id">The ID of the product category to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private List<CategoryDTO> MapCategoriesToCategoryDto(IEnumerable<Category> categories)
        {
            return categories.Select(MapCategoryToCategoryDto).ToList();
        }

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
