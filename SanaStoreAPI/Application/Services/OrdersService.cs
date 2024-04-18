using DB.Models;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using SanaStoreAPI.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        /// Constructs an OrdersService with necessary dependencies.
        /// </summary>
        /// <param name="ordersRepository">Repository to access order data.</param>
        /// <param name="productsRepository">Repository to access product data.</param>
        public OrdersService(IOrdersRepository ordersRepository, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all order DTOs.</returns>
        /// <exception cref="ApplicationException">Thrown if an error occurs during database access.</exception>
        public async Task<IEnumerable<OrdersDTO>> GetOrders()
        {
            try
            {
                return await _ordersRepository.GetOrders();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving orders", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific order by ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the order DTO if found, or null otherwise.</returns>
        /// <exception cref="ApplicationException">Thrown if an error occurs while retrieving the order.</exception>
        public async Task<OrdersDTO> GetOrder(int id)
        {
            try
            {
                var orderDto = await _ordersRepository.GetOrder(id);
                if (orderDto == null)
                {
                    return null;
                }
                return orderDto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving order with ID {id}: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Creates a new order based on the provided order DTO and the user ID.
        /// </summary>
        /// <param name="orderDto">Data transfer object containing the new order details.</param>
        /// <param name="userId">ID of the user creating the order, to be set as the customer ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the newly created order DTO.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is insufficient stock for any product in the order.</exception>
        /// <exception cref="ApplicationException">Thrown if an error occurs while creating the order.</exception>
        public async Task<OrdersDTO> CreateOrder(OrdersDTO orderDto, int userId)
        {
            try
            {
                orderDto.CustomerID = userId;
                var order = MapDtoToOrder(orderDto);

                foreach (var detail in order.OrderDetails)
                {
                    var productDto = await _productsRepository.GetProduct(detail.ProductID);
                    if (productDto == null || productDto.Stock < detail.Quantity)
                    {
                        throw new InvalidOperationException($"Stock insuficiente para el producto ID {detail.ProductID}.");
                    }

                    var product = MapDtoToProduct(productDto);
                    product.Stock -= detail.Quantity;
                    await _productsRepository.UpdateProduct(product.ProductID, product);
                }

                await _ordersRepository.CreateOrder(order);
                return MapOrderToDto(order);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating order", ex);
            }
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">ID of the order to update.</param>
        /// <param name="orderDto">Data transfer object containing updated order details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the update was successful.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the order to be updated is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there is insufficient stock to accommodate the updated order.</exception>
        /// <exception cref="ApplicationException">Thrown if an error occurs during the update process.</exception>
        public async Task<bool> UpdateOrder(int id, OrdersDTO orderDto)
        {
            try
            {
                var existingOrder = await _ordersRepository.GetOrder(id);
                if (existingOrder == null) throw new KeyNotFoundException($"Order with ID {id} not found.");

                var currentDetails = existingOrder.OrderDetails.ToList();
                foreach (var detailDto in orderDto.OrderDetails)
                {
                    var product = await _productsRepository.GetProduct(detailDto.ProductID);
                    if (product == null)
                    {
                        throw new KeyNotFoundException($"Product with ID {detailDto.ProductID} not found.");
                    }

                    var extraNeeded = detailDto.Quantity - (currentDetails.FirstOrDefault(d => d.ProductID == detailDto.ProductID)?.Quantity ?? 0);
                    if (product.Stock < extraNeeded)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product ID {detailDto.ProductID}.");
                    }
                }

                var orderToUpdate = MapDtoToOrder(orderDto);
                foreach (var detail in orderToUpdate.OrderDetails)
                {
                    var productDto = await _productsRepository.GetProduct(detail.ProductID);
                    var product = MapDtoToProduct(productDto);
                    var originalDetail = currentDetails.FirstOrDefault(d => d.ProductID == detail.ProductID);

                    product.Stock += originalDetail?.Quantity - detail.Quantity ?? -detail.Quantity;
                    await _productsRepository.UpdateProduct(product.ProductID, product);
                }

                return await _ordersRepository.UpdateOrder(id, orderToUpdate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating order", ex);
            }
        }

        /// <summary>
        /// Deletes an order based on the ID.
        /// </summary>
        /// <param name="id">ID of the order to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ApplicationException">Thrown if an error occurs during the deletion process.</exception>
        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                return await _ordersRepository.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting order", ex);
            }
        }

        private Orders MapDtoToOrder(OrdersDTO dto)
        {
            var order = new Orders
            {
                OrderID = dto.OrderID,  // Solo se asigna si es una actualización, para creación debería ser ignorado o cero.
                CustomerID = dto.CustomerID,
                OrderDate = dto.OrderDate,
                OrderDetails = dto.OrderDetails.Select(od => new OrderDetail
                {
                    OrderDetailID = od.OrderDetailID,  // Como antes, solo para actualización.
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList(),
                Customer = new Customers  // Siempre asigna el cliente directamente
                {
                    CustomerID = dto.Customer.CustomerID, // Puede ser cero si es un nuevo cliente y la ID se genera automáticamente
                    FirstName = dto.Customer.FirstName,
                    LastName = dto.Customer.LastName,
                    Email = dto.Customer.Email
                }
            };

            return order;
        }

        private Products MapDtoToProduct(ProductsDTO dto)
        {
            return new Products
            {
                ProductID = dto.ProductID,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Image = dto.Image,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        private OrdersDTO MapOrderToDto(Orders order)
        {
            return new OrdersDTO
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.OrderDetailID,
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList(),
                Customer = new CustomersDTO
                {
                    CustomerID = order.Customer.CustomerID,
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    Email = order.Customer.Email
                }
            };
        }

    }
}
