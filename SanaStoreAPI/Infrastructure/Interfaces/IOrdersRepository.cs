using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Data.Interfaces
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrdersDTO>> GetOrders();
        Task<OrdersDTO> GetOrder(int id);
        Task<OrdersDTO> CreateOrder(Orders order);
        Task<bool> UpdateOrder(int id, Orders order);
        Task<bool> DeleteOrder(int id);
    }
}
