using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Pagination;
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
        public async Task<ActionResult<CreateProductResult>> Post([FromBody] CreateProductCommand command)
        {
            if (await mediator.Send(command) is not { } result)
                return BadRequest("Erro ao criar produto.");

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = productRepository.GetAllQueryable();

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginationResponse<Product>(
                products, totalCount, page, pageSize
            );

            return Ok(response);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command, Guid id)
        {
            command.Id = id;
            
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return Ok(new { message = "Produto excluído com sucesso." });
        }
    }
}