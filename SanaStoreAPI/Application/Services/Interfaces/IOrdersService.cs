using SanaStoreAPI.Domain.Models;

public interface IOrdersService
{
    Task<IEnumerable<OrdersDTO>> GetOrders();
    Task<OrdersDTO> GetOrder(int id);
    Task<OrdersDTO> CreateOrder(OrdersDTO orderDto, int userId);
    Task<bool> UpdateOrder(int id, OrdersDTO orderDto);
    Task<bool> DeleteOrder(int id);
}