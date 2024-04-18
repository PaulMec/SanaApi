using Microsoft.AspNetCore.Mvc;
using SanaStoreAPI.Infrastructure.Services.Interfaces;
using SanaStoreAPI.Domain.Models;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    /// <summary>
    /// Initializes a new instance of the CustomersController.
    /// </summary>
    /// <param name="customersService">Service to manage customer data.</param>
    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>An ActionResult containing all customers or an error message if an exception occurs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var customers = await _customersService.GetCustomers();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a single customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns>An ActionResult containing the customer data or NotFound if the customer does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var customer = await _customersService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a customer by their email address.
    /// </summary>
    /// <param name="email">The email address of the customer to retrieve.</param>
    /// <returns>An ActionResult containing the customer data or NotFound if no customer is found with that email.</returns>
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var customer = await _customersService.GetCustomerByEmail(email);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">Data transfer object containing the customer data to create.</param>
    /// <returns>A CreatedAtAction result with the new customer data if successful, or an error status if an exception occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomersDTO customerDto)
    {
        try
        {
            var createdCustomer = await _customersService.CreateCustomer(customerDto);
            return CreatedAtAction(nameof(Get), new { id = createdCustomer.CustomerID }, createdCustomer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customerDto">Data transfer object containing the updated data for the customer.</param>
    /// <returns>A NoContent result if successful, NotFound if the customer does not exist, or an error status if an exception occurs.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomersDTO customerDto)
    {
        try
        {
            var success = await _customersService.UpdateCustomer(id, customerDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes an existing customer.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>A NoContent result if successful, NotFound if the customer does not exist, or an error status if an exception occurs.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _customersService.DeleteCustomer(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
