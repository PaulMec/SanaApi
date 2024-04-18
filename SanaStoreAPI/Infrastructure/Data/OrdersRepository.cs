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
    /// Repository for managing orders.
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        private readonly SanaStoreContext _context;

        public OrdersRepository(SanaStoreContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public async Task<IEnumerable<OrdersDTO>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            var orderDtos = MapOrdersToOrderDto(orders);

            return orderDtos;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID.</returns>
        public async Task<OrdersDTO> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return null;
            }

            var orderDto = MapOrderToOrderDto(order);

            return orderDto;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The newly created order.</returns>
        public async Task<OrdersDTO> CreateOrder(Orders order)
        {
            // Verifica si el cliente existe
            var customer = await _context.Customers.FindAsync(order.CustomerID);
            if (customer == null)
            {
                throw new ArgumentException("Customer ID not found");
            }

            order.Customer = customer;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return MapOrderToOrderDto(order);
        }


        /// <inheritdoc/>
        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order data.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateOrder(int id, Orders order)
        {
            if (id != order.OrderID)
            {
                return false;
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        private List<OrdersDTO> MapOrdersToOrderDto(IEnumerable<Orders> orders)
        {
            return orders.Select(MapOrderToOrderDto).ToList();
        }

        private OrdersDTO MapOrderToOrderDto(Orders order)
        {
            return new OrdersDTO
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                Customer = new CustomersDTO
                {
                    CustomerID = order.Customer.CustomerID,
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    Email = order.Customer.Email
                },
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.OrderDetailID,
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };
        }
    }
}
