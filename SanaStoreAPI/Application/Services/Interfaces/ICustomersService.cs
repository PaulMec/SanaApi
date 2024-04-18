using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<IEnumerable<CustomersDTO>> GetCustomers();
        Task<CustomersDTO> GetCustomer(int id);
        Task<CustomersDTO> GetCustomerByEmail(string email);
        Task<CustomersDTO> CreateCustomer(CustomersDTO customer);
        Task<bool> UpdateCustomer(int id, CustomersDTO customer);
        Task<bool> DeleteCustomer(int id);
    }
}
