using SanaStoreAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanaStoreAPI.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDTO>> GetBasicProductInfoListAsync(int pageNumber, int pageSize);
    }
}
