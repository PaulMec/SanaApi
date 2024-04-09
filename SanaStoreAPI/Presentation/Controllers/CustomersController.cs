using DB.Context;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanaStoreAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SanaStoreContext _context;

        public CustomersController(SanaStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersDTO>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            var customerDtos = MapCustomersToCustomerDto(customers);
            return Ok(customerDtos);
        }

        /// <summary>
        /// Retrieves a specific customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomersDTO>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = MapCustomerToCustomerDto(customer);

            return Ok(customerDto);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The newly created customer.</returns>
        [HttpPost]
        public async Task<ActionResult<CustomersDTO>> CreateCustomer(Customers customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The updated customer data.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customers customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        /// <summary>
        /// Maps a collection of customer entities to a list of customer DTOs.
        /// </summary>
        /// <param name="customers">The collection of customer entities to map.</param>
        /// <returns>A list of mapped customer DTOs.</returns>
        private List<CustomersDTO> MapCustomersToCustomerDto(IEnumerable<Customers> customers)
        {
            return customers.Select(MapCustomerToCustomerDto).ToList();
        }

        /// <summary>
        /// Maps a customer entity to a customer DTO.
        /// </summary>
        /// <param name="customer">The customer entity to map.</param>
        /// <returns>The mapped customer DTO.</returns>
        private CustomersDTO MapCustomerToCustomerDto(Customers customer)
        {
            return new CustomersDTO
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }
    }
}
