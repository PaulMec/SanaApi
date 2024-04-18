using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Data.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> CreateCategory(Category category);
        Task<bool> UpdateCategory(int id, Category category);
        Task<bool> DeleteCategory(int id);
    }
}
