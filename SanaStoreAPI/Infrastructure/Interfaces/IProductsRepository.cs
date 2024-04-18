using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Data.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductsDTO>> GetProducts(int pageNumber, int pageSize);
        Task<ProductsDTO> GetProduct(int id);
        Task<ProductsDTO> CreateProduct(Products product);
        Task<bool> UpdateProduct(int id, Products product);
        Task<bool> DeleteProduct(int id);
    }
}
