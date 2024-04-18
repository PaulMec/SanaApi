using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> CreateCategory(CategoryDTO categoryDto);
        Task<bool> UpdateCategory(int id, CategoryDTO categoryDto);
        Task<bool> DeleteCategory(int id);
    }
}
