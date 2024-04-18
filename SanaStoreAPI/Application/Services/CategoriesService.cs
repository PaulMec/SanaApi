using DB.Models;
using SanaStoreAPI.Domain.Models;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using SanaStoreAPI.Infrastructure.Services.Interfaces;
using System;


namespace SanaStoreAPI.Infrastructure.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        /// <summary>
        /// Constructs an CategoriesService with necessary dependencies.
        /// </summary>
        /// <param name="categoriesRepository">The repository handling category data operations.</param>
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Retrieves all categories from the repository.
        /// </summary>
        /// <returns>A list of all category DTOs.</returns>
        /// <exception cref="Exception">Thrown when no categories are found or there is an error retrieving the categories.</exception>
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _categoriesRepository.GetCategories();

            if (categories == null || !categories.Any())
            {
                throw new Exception("No categories found.");
            }

            return categories;
        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category DTO if found; otherwise, throws an exception.</returns>
        /// <exception cref="Exception">Thrown if the category with the specified ID is not found or there is an error retrieving the category.</exception>
        public async Task<CategoryDTO> GetCategory(int id)
        {
            var category = await _categoriesRepository.GetCategory(id);

            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            return category;
        }

        /// <summary>
        /// Creates a new category based on the provided category DTO.
        /// </summary>
        /// <param name="categoryDto">The category data transfer object containing the name of the category.</param>
        /// <returns>The newly created category DTO.</returns>
        /// <exception cref="Exception">Thrown if the category name is missing or there is an error during the creation process.</exception>
        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.CategoryName))
            {
                throw new Exception("Category name is required.");
            }

            var category = new Category
            {
                CategoryName = categoryDto.CategoryName
            };

            var createdCategory = await _categoriesRepository.CreateCategory(category);

            return new CategoryDTO
            {
                CategoryID = createdCategory.CategoryID,
                CategoryName = createdCategory.CategoryName
            };
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The category DTO containing the updated details.</param>
        /// <returns>True if the update was successful; otherwise, throws an exception.</returns>
        /// <exception cref="ArgumentException">Thrown if the given ID is not valid.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the category with the specified ID is not found.</exception>
        /// <exception cref="ArgumentException">Thrown if the category name is missing.</exception>
        /// <exception cref="Exception">Thrown if an error occurs during the update process.</exception>
        public async Task<bool> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value.");
            }

            var existingCategory = await _categoriesRepository.GetCategory(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(categoryDto.CategoryName))
            {
                throw new ArgumentException("Category name is required.");
            }

            var categoryToUpdate = MapDtoToCategory(categoryDto);
            categoryToUpdate.CategoryID = id;

            var updateSuccessful = await _categoriesRepository.UpdateCategory(id, categoryToUpdate);
            if (!updateSuccessful)
            {
                throw new Exception("An error occurred while updating the category.");
            }

            return true;
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, throws an exception.</returns>
        /// <exception cref="Exception">Thrown if the category with the specified ID is not found or there is an error during the deletion process.</exception>
        public async Task<bool> DeleteCategory(int id)
        {
            var existingCategory = await _categoriesRepository.GetCategory(id);
            if (existingCategory == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            return await _categoriesRepository.DeleteCategory(id);
        }

        private Category MapDtoToCategory(CategoryDTO dto)
        {
            return new Category
            {
                CategoryID = dto.CategoryID,
                CategoryName = dto.CategoryName
            };
        }
    }
}