using Microsoft.AspNetCore.Mvc;
using SanaStoreAPI.Infrastructure.Services.Interfaces;
using SanaStoreAPI.Domain.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    /// <summary>
    /// Constructor for the OrdersController.
    /// </summary>
    /// <param name="ordersService">The service responsible for order operations.</param>
    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of all orders in the system.</returns>
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _ordersService.GetOrders();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific order by ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>The specified order if found; otherwise, a not found result.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _ordersService.GetOrder(id);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound($"Order with ID {id} not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderDto">The order data transfer object containing information about the new order.</param>
    /// <returns>A created result with the new order if successful; otherwise, returns a status indicating the failure.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrdersDTO orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var order = await _ordersService.CreateOrder(orderDto, orderDto.CustomerID);
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="orderDto">The updated order data.</param>
    /// <returns>An Ok result if the update is successful; otherwise, a bad request or server error result.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrdersDTO orderDto)
    {
        try
        {
            var success = await _ordersService.UpdateOrder(id, orderDto);
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to update order.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes an order by ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>An Ok result if the deletion is successful; otherwise, a not found or server error result.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            var success = await _ordersService.DeleteOrder(id);
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound($"Order with ID {id} not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
