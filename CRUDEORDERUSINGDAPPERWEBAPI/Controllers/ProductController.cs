using CRUDEORDERUSINGDAPPERWEBAPI.Model;
using CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProudct(Product product)
        {
            var prod=await _productRepo.AddNewProduct(product); 
            return Ok(prod);    
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProudct(Product product)
        {
            var prod= await _productRepo.UpdateProduct(product);

            return Ok(prod);
        }
    }
}
