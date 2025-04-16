using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Product;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var product = new Domain.Entities.Product(
                request.Name,
                request.Price,
                request.Quantity,
                request.Description
            );

            await productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productRepository.GetAllAsync();
            return Ok(products);
        }
    }
}