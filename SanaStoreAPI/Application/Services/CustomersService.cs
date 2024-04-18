using DB.Models;
using SanaStoreAPI.Domain.Models;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using SanaStoreAPI.Infrastructure.Services.Interfaces;

namespace SanaStoreAPI.Infrastructure.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        /// <summary>
        /// Constructs a CustomersService with necessary repository dependency.
        /// </summary>
        /// <param name="customersRepository">The repository that handles customer data operations.</param>
        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        /// <summary>
        /// Retrieves all customers from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all customer DTOs.</returns>
        /// <exception cref="Exception">Thrown when no customers are found or there is an error retrieving the customers.</exception>
        public async Task<IEnumerable<CustomersDTO>> GetCustomers()
        {
            try
            {
                var customers = await _customersRepository.GetCustomers();

                if (customers == null || !customers.Any())
                {
                    throw new Exception("No customers found.");
                }

                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customers", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer DTO if found; otherwise, throws an exception.</returns>
        /// <exception cref="Exception">Thrown if the customer with the specified ID is not found or there is an error retrieving the customer.</exception>
        public async Task<CustomersDTO> GetCustomer(int id)
        {
            try
            {
                var customer = await _customersRepository.GetCustomer(id);

                if (customer == null)
                {
                    throw new Exception($"Customer with ID {id} not found.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer with ID: {id}", ex);
            }
        }

        /// <summary>
        /// Retrieves a customer by their email address.
        /// </summary>
        /// <param name="email">The email address of the customer to retrieve.</param>
        /// <returns>The customer DTO if found; otherwise, throws an exception.</returns>
        /// <exception cref="Exception">Thrown if the customer is not found by email or there is an error retrieving the customer.</exception>
        public async Task<CustomersDTO> GetCustomerByEmail(string email)
        {
            try
            {
                var customer = await _customersRepository.GetCustomerByEmail(email);
                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer by email", ex);
            }
        }


        /// <summary>
        /// Creates a new customer based on the provided customer DTO.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object containing the customer's details.</param>
        /// <returns>The newly created customer DTO.</returns>
        /// <exception cref="ArgumentException">Thrown if any customer information is incomplete.</exception>
        /// <exception cref="Exception">Thrown if there is an error during the creation process.</exception>
        public async Task<CustomersDTO> CreateCustomer(CustomersDTO customerDto)
        {
            ValidateCustomerData(customerDto);

            var customer = new Customers
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email
            };

            var createdCustomer = await _customersRepository.CreateCustomer(customer);

            return new CustomersDTO
            {
                CustomerID = createdCustomer.CustomerID,
                FirstName = createdCustomer.FirstName,
                LastName = createdCustomer.LastName,
                Email = createdCustomer.Email
            };
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerDto">The customer DTO containing the updated details.</param>
        /// <returns>True if the update is successful; otherwise, throws an exception.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the customer with the specified ID is not found.</exception>
        /// <exception cref="ArgumentException">Thrown if any customer information is incomplete.</exception>
        /// <exception cref="Exception">Thrown if there is an error updating the customer.</exception>
        public async Task<bool> UpdateCustomer(int id, CustomersDTO customerDto)
        {
            try
            {
                var existingCustomer = await _customersRepository.GetCustomer(id);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                }

                if (string.IsNullOrWhiteSpace(customerDto.FirstName) ||
                    string.IsNullOrWhiteSpace(customerDto.LastName) ||
                    string.IsNullOrWhiteSpace(customerDto.Email))
                {
                    throw new ArgumentException("Customer information is incomplete.");
                }

                var customerToUpdate = MapDtoToCustomer(customerDto);
                customerToUpdate.CustomerID = existingCustomer.CustomerID;
                return await _customersRepository.UpdateCustomer(id, customerToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating customer with ID: {id}", ex);
            }
        }

        /// <summary>
        /// Deletes a customer based on the ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, throws an exception.</returns>
        /// <exception cref="Exception">Thrown if the customer with the specified ID is not found or there is an error during the deletion process.</exception>
        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var existingCustomer = await _customersRepository.GetCustomer(id);
                if (existingCustomer == null)
                {
                    throw new Exception($"Customer with ID {id} not found.");
                }

                return await _customersRepository.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting customer with ID: {id}", ex);
            }
        }

        private Customers MapDtoToCustomer(CustomersDTO dto)
        {
            return new Customers
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }
        private void ValidateCustomerData(CustomersDTO customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Customer information is incomplete.");
            }
        }
    }
}
