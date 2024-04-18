using DB.Context;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Application.Services.Interfaces;
using SanaStoreAPI.Domain.Models;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace SanaStoreAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        /// Constructs an ProductService with necessary dependencies.
        /// </summary>
        /// <param name="productsRepository">The repository that handles the product data operations.</param>
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Retrieves a paginated list of product information.
        /// </summary>
        /// <param name="pageNumber">The current page number of the pagination.</param>
        /// <param name="pageSize">The number of items to include in one page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products as <see cref="ProductsDTO"/>.</returns>
        /// <exception cref="Exception">Thrown when there is a failure to retrieve products from the database or any other general exception occurs.</exception>
        public async Task<IEnumerable<ProductsDTO>> GetBasicProductInfoListAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _productsRepository.GetProducts(pageNumber, pageSize);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error retrieving products from database", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products", ex);
            }
        }
    }
}
