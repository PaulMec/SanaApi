using DB.Models;
using SanaStoreAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Infrastructure.Data.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<CustomersDTO>> GetCustomers();
        Task<CustomersDTO> GetCustomer(int id);
        Task<CustomersDTO> GetCustomerByEmail(string email);
        Task<CustomersDTO> CreateCustomer(Customers customer);
        Task<bool> UpdateCustomer(int id, Customers customer);
        Task<bool> DeleteCustomer(int id);
    }
}
