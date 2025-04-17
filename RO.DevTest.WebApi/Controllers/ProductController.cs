using MediatR;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand;
using RO.DevTest.Application.Features.Products.Commands.DeleteProductsCommand;
using RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController(IMediator mediator, IProductRepository productRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var product = new Domain.Entities.Product(
                command.Name,
                command.Price,
                command.Quantity,
                command.Description
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command, Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Produto não encontrado para atualização.");
            }

            product.Name = command.Name;
            product.Price = command.Price;
            product.Quantity = command.Quantity;
            product.Description = command.Description;

            await productRepository.UpdateAsync(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}