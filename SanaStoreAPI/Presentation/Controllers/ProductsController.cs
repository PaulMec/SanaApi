using Microsoft.AspNetCore.Mvc;
using SanaStoreAPI.Application.Services.Interfaces;
using SanaStoreAPI.Domain.Models;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="productService">The service that provides product operations.</param>
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="pageNumber">The page number of the product list to retrieve, with a default value of 1.</param>
    /// <param name="pageSize">The number of products per page, with a default value of 15.</param>
    /// <returns>An <see cref="IActionResult"/> that contains a list of products. Returns a status code of 200 (OK) with the product list if successful, or a status code of 500 (Internal Server Error) if an exception occurs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 15)
    {
        try
        {
            var products = await _productService.GetBasicProductInfoListAsync(pageNumber, pageSize);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
