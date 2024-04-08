using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SanaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private SanaStoreContext _context;

        public ProductsController(SanaStoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Products> GetProducts() => _context.Products.ToList();
    }
}
