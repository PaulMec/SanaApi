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
    /// Repository for managing customers.
    /// </summary>
    public class CustomersRepository : ICustomersRepository
    {
        private readonly SanaStoreContext _context;

        public CustomersRepository(SanaStoreContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        public async Task<IEnumerable<CustomersDTO>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            var customerDtos = MapCustomersToCustomerDto(customers);
            return customerDtos;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Retrieves a specific customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer with the specified ID.</returns>
        public async Task<CustomersDTO> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            var customerDto = MapCustomerToCustomerDto(customer);

            return customerDto;
        }

        public async Task<CustomersDTO> GetCustomerByEmail(string email)
        {
            var customer = await _context.Customers
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return null;
            }

            return MapCustomerToCustomerDto(customer);
        }


        /// <inheritdoc/>
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The newly created customer.</returns>
        public async Task<CustomersDTO> CreateCustomer(Customers customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return MapCustomerToCustomerDto(customer);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The updated customer data.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateCustomer(int id, Customers customer)
        {
            if (id != customer.CustomerID)
            {
                return false;
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }


        /// <inheritdoc/>
        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private List<CustomersDTO> MapCustomersToCustomerDto(IEnumerable<Customers> customers)
        {
            return customers.Select(MapCustomerToCustomerDto).ToList();
        }

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
