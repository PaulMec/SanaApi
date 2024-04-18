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
    /// Repository for managing products.
    /// </summary>
    public class ProductsRepository : IProductsRepository
    {
        private readonly SanaStoreContext _context;

        /// <summary>
        /// Constructor for ProductsRepository.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductsRepository(SanaStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public async Task<IEnumerable<ProductsDTO>> GetProducts(int pageNumber, int pageSize)
        {
            var products = await _context.Products
                                         .Include(p => p.ProductCategories)
                                            .ThenInclude(pc => pc.Category)
                                         .OrderBy(p => p.ProductID)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            var productDtos = MapProductsToProductDto(products);
            return productDtos;
        }


        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        public async Task<ProductsDTO> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            var productDto = MapProductToProductDto(product);

            return productDto;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The newly created product.</returns>
        public async Task<ProductsDTO> CreateProduct(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return MapProductToProductDto(product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>True if the product was successfully updated, otherwise false.</returns>
        public async Task<bool> UpdateProduct(int id, Products updatedProductData)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
            {
                return false;
            }

            // Update the properties
            product.ProductName = updatedProductData.ProductName;
            product.Description = updatedProductData.Description;
            product.Price = updatedProductData.Price;
            product.Stock = updatedProductData.Stock;
            product.Image = updatedProductData.Image;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }




        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was successfully deleted, otherwise false.</returns>
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        private List<ProductsDTO> MapProductsToProductDto(IEnumerable<Products> products)
        {
            return products.Select(MapProductToProductDto).ToList();
        }

        private ProductsDTO MapProductToProductDto(Products product)
        {
            return new ProductsDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Stock = product.Stock,
                Categories = product.ProductCategories?.Select(pc => new CategoryDTO
                {
                    CategoryID = pc.Category.CategoryID,
                    CategoryName = pc.Category.CategoryName
                }).ToList() ?? new List<CategoryDTO>()
            };
        }

    }
}
