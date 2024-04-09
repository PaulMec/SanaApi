using DB.Context;
using DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Presentation.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SanaStoreContext _context;

        public ProductsController(SanaStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productDtos = MapProductsToProductDto(products);
            return Ok(productDtos);
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = MapProductToProductDto(product);

            return Ok(productDto);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        /// <summary>
        /// Maps a collection of product entities to a list of product DTOs.
        /// </summary>
        /// <param name="products">The collection of product entities to map.</param>
        /// <returns>A list of mapped product DTOs.</returns>
        private List<ProductsDTO> MapProductsToProductDto(IEnumerable<Products> products)
        {
            return products.Select(MapProductToProductDto).ToList();
        }

        /// <summary>
        /// Maps a product entity to a product DTO.
        /// </summary>
        /// <param name="product">The product entity to map.</param>
        /// <returns>The mapped product DTO.</returns>
        private ProductsDTO MapProductToProductDto(Products product)
        {
            return new ProductsDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
