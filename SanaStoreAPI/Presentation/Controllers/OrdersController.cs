using DB.Context;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Presentation.Controllers
{
    /// <summary>
    /// API controller for managing orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SanaStoreContext _context;

        public OrdersController(SanaStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            var orderDtos = MapOrdersToOrderDto(orders);

            return Ok(orderDtos);
        }

        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDTO>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = MapOrderToOrderDto(order);

            return orderDto;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The newly created order.</returns>
        [HttpPost]
        public async Task<ActionResult<Orders>> CreateOrder(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order data.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Orders order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        /// Deletes an order.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if an order with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the order to check.</param>
        /// <returns>True if an order with the specified ID exists, otherwise, false.</returns>
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }

        /// <summary>
        /// Maps a collection of order entities to a list of order DTOs.
        /// </summary>
        /// <param name="orders">The collection of order entities to map.</param>
        /// <returns>A list of mapped order DTOs.</returns>
        private List<OrdersDTO> MapOrdersToOrderDto(IEnumerable<Orders> orders)
        {
            return orders.Select(MapOrderToOrderDto).ToList();
        }

        /// <summary>
        /// Maps an order entity to an order DTO.
        /// </summary>
        /// <param name="order">The order entity to map.</param>
        /// <returns>The mapped order DTO.</returns>
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
