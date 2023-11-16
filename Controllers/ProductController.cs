using CatalogSpa.API.Models;
using CatalogSpa.API.Repositories;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;

namespace CatalogSpa.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts()
        {
            return Ok(await productRepository.GetProducts());
        }

        [HttpGet]
        [Route("[action]/{id:length(24)}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var result = await productRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            return Ok(await productRepository.GetProductByCategory(category));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await productRepository.CreateProduct(product);
            return Ok(product);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            return Ok(await productRepository.UpdateProduct(product));
        }

        [HttpDelete]
        [Route("[action]/{id:length(24)}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            return Ok(await productRepository.DeleteProduct(id));
        }
    }
}
